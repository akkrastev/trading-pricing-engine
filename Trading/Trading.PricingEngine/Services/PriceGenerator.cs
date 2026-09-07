using Microsoft.Extensions.Options;
using Trading.PricingEngine.Configuration;
using Trading.PricingEngine.Models;
using Trading.PricingEngine.Services.Interfaces;

namespace Trading.PricingEngine.Services;

public class PriceGenerator : IPriceGenerator
{
    private const decimal MinimumPriceIncrement = 0.0001m;

    private readonly PricingOptions _options;

    public PriceGenerator(IOptions<PricingOptions> options)
    {
        _options = options.Value;
    }

    public (decimal NextPrice, PriceTick Tick) Generate(string symbol, decimal currentPrice)
    {
        var movePercent = (decimal)(Random.Shared.NextDouble() * 2 - 1) * _options.MaxPriceMovePercent;
        var nextPrice = currentPrice * (1 + movePercent / 100m);

        var spreadPercent = _options.MinSpreadPercent +
            (decimal)Random.Shared.NextDouble() * (_options.MaxSpreadPercent - _options.MinSpreadPercent);
        var halfSpread = nextPrice * spreadPercent / 100m / 2m;

        var bidPrice = Math.Round(nextPrice - halfSpread, 4, MidpointRounding.AwayFromZero);
        var askPrice = Math.Round(nextPrice + halfSpread, 4, MidpointRounding.AwayFromZero);

        if (askPrice <= bidPrice)
            askPrice = bidPrice + MinimumPriceIncrement;

        var tick = new PriceTick
        {
            Symbol = symbol,
            BidPrice = bidPrice,
            AskPrice = askPrice,
            Timestamp = DateTime.UtcNow,
        };

        return (nextPrice, tick);
    }
}