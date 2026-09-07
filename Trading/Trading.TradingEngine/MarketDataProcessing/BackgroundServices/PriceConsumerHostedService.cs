using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using Trading.PricingEngine.Models;
using Trading.TradingEngine.AutoTrading.Services.Interfaces;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;
using Trading.TradingEngine.Orders.Models;

namespace Trading.TradingEngine.MarketDataProcessing.BackgroundServices;

public class PriceConsumerHostedService : BackgroundService
{
    private readonly ChannelReader<PriceTick> _priceReader;
    private readonly IPriceProcessor _priceProcessor;
    private readonly IAutoTradingEngine _autoTradingEngine;

    private readonly ChannelWriter<ProcessedOrder> _orderWriter;

    public PriceConsumerHostedService(ChannelReader<PriceTick> reader, IPriceProcessor priceProcessor, IAutoTradingEngine autoTradingEngine, ChannelWriter<ProcessedOrder> orderWriter)
    {
        _priceReader = reader;
        _priceProcessor = priceProcessor;
        _autoTradingEngine = autoTradingEngine;
        _orderWriter = orderWriter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var tick in _priceReader.ReadAllAsync(stoppingToken))
        {
            var state = _priceProcessor.Process(tick);
            var processedOrder = _autoTradingEngine.Process(state);

            if (processedOrder is not null)
            {
                await _orderWriter.WriteAsync(processedOrder, stoppingToken);
            }
        }
    }
}