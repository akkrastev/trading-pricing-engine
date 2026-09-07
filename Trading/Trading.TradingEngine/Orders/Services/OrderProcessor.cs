using Trading.TradingEngine.MarketDataProcessing.Services;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.Orders.Services.Interfaces;
using Trading.TradingEngine.TradingRules;
using Trading.TradingEngine.TradingRules.Models;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.TradingEngine.Orders.Services;

public class OrderProcessor : IOrderProcessor
{
    private readonly TradingRulesStore _tradingRulesStore;
    private readonly PriceStateStore _priceStateStore;
    private readonly ITradingRulesEngine _tradingRulesEngine;

    public OrderProcessor(
        TradingRulesStore tradingRulesStore,
        PriceStateStore priceStateStore,
        ITradingRulesEngine tradingRulesEngine)
    {
        _tradingRulesStore = tradingRulesStore;
        _priceStateStore = priceStateStore;
        _tradingRulesEngine = tradingRulesEngine;
    }

    public ProcessedOrder Process(OrderRequest request)
    {
        var rules = _tradingRulesStore.GetCurrent();
        return Process(request, rules);
    }

    public ProcessedOrder Process(OrderRequest request, TradingRulesSnapshot rules)
    {
        var state = _priceStateStore.GetState(request.Symbol);

        var decision = _tradingRulesEngine.Evaluate(request, rules, state?.Current);

        return new ProcessedOrder
        {
            Request = request,
            Decision = decision,
            RulesVersion = rules.Version,
        };
    }
}
