using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Trading.PricingEngine.Configuration;
using Trading.PricingEngine.MarketDataSimulation.Data;
using Trading.PricingEngine.Models;
using Trading.PricingEngine.Services.Interfaces;

namespace Trading.PricingEngine.MarketDataSimulation.BackgroundServices;

public class MarketDataGenerator : BackgroundService
{
    private readonly ChannelWriter<PriceTick> _writer;
    private readonly IPriceGenerator _priceGenerator;
    private readonly IPriceTickValidator _priceTickValidator;
    private readonly PricingOptions _options;

    public MarketDataGenerator(
        ChannelWriter<PriceTick> writer,
        IPriceGenerator priceGenerator,
        IPriceTickValidator priceTickValidator,
        IOptions<PricingOptions> options)
    {
        _writer = writer;
        _priceGenerator = priceGenerator;
        _priceTickValidator = priceTickValidator;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var producerCts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
        Exception? completionError = null;

        try
        {
            var producers = _options.Symbols.Select(symbol => ProduceAsync(symbol, producerCts));
            await Task.WhenAll(producers);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {

        }
        catch (Exception ex)
        {
            completionError = ex;
            throw;
        }
        finally
        {
            _writer.TryComplete(completionError);
        }
    }

    private async Task ProduceAsync(string symbol, CancellationTokenSource producerCts)
    {
        var token = producerCts.Token;
        var currentPrice = SymbolBasePrices.StartingPrices[symbol];

        try
        {
            while (!token.IsCancellationRequested)
            {
                var (nextPrice, tick) = _priceGenerator.Generate(symbol, currentPrice);
                currentPrice = nextPrice;

                if (!_priceTickValidator.IsValid(tick))
                    throw new InvalidOperationException(
                        $"Invalid price tick generated for symbol '{symbol}'.");

                await _writer.WriteAsync(tick, token);
                await Task.Delay(_options.TickIntervalMs, token);
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {

        }
        catch
        {
            producerCts.Cancel();
            throw;
        }
    }
}