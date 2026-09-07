using Microsoft.Extensions.Options;
using Trading.TradingEngine.AutoTrading.Services.Interfaces;
using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.Orders.Services.Interfaces;

namespace Trading.TradingEngine.AutoTrading.Services;

public class AutoTradingEngine : IAutoTradingEngine
{
    private readonly IOrderProcessor _orderProcessor;
    private readonly AutoTradingOptions _options;

    public AutoTradingEngine(IOrderProcessor orderProcessor, IOptions<AutoTradingOptions> options)
    {
        _orderProcessor = orderProcessor;
        _options = options.Value;
    }

    public ProcessedOrder? Process(SymbolState state)
    {
        if (state.Previous is null)
        {
            return null;
        }

        if (state.Current.SpreadPercent <= _options.SpreadPercentThreshold)
        {
            return null;
        }

        var current = state.Current.CurrentMarketPrice;
        var previous = state.Previous.CurrentMarketPrice;

        if (current == previous)
        {
            return null;
        }

        var side = current > previous ? OrderSide.Sell : OrderSide.Buy;

        var orderPrice = side == OrderSide.Sell
            ? state.Current.AskPrice - state.Current.AskPrice * 0.0003m
            : state.Current.BidPrice + state.Current.BidPrice * 0.0003m;

        var request = new OrderRequest
        {
            Id = Guid.NewGuid(),
            Symbol = state.Current.Symbol,
            Side = side,
            Price = orderPrice,
            Quantity = _options.Quantity,
            Source = OrderSource.Automatic,
            Timestamp = DateTime.UtcNow,
        };

        return _orderProcessor.Process(request);
    }
}
