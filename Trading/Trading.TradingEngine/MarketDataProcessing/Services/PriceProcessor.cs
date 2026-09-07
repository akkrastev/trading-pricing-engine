using Trading.PricingEngine.Models;
using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

namespace Trading.TradingEngine.MarketDataProcessing.Services;

public class PriceProcessor : IPriceProcessor
{
    private readonly IPriceMath _priceMath;
    private readonly IPriceStateStore _priceStateStore;

    public PriceProcessor(IPriceMath priceMath, IPriceStateStore priceStateStore)
    {
        _priceMath = priceMath;
        _priceStateStore = priceStateStore;
    }

    public SymbolState Process(PriceTick tick)
    {
        var midPrice = _priceMath.CalculateMidPrice(tick.BidPrice, tick.AskPrice);
        var spread = _priceMath.CalculateSpread(tick.BidPrice, tick.AskPrice);
        var spreadPercent = _priceMath.CalculateSpreadPercent(spread, midPrice);

        var snapshot = new MarketSnapshot
        {
            Symbol = tick.Symbol,
            BidPrice = tick.BidPrice,
            AskPrice = tick.AskPrice,
            CurrentMarketPrice = midPrice,
            Spread = spread,
            SpreadPercent = spreadPercent,
            Timestamp = tick.Timestamp,
        };

        var previousState = _priceStateStore.GetState(tick.Symbol);

        var newState = new SymbolState
        {
            Current = snapshot,
            Previous = previousState?.Current,
        };

        _priceStateStore.SetState(tick.Symbol, newState);

        return newState;
    }
}