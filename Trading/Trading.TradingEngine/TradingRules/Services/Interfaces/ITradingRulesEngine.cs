using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.TradingRules.Models;

namespace Trading.TradingEngine.TradingRules.Services.Interfaces;

public interface ITradingRulesEngine
{
    OrderDecision Evaluate(OrderRequest request,TradingRulesSnapshot rules, MarketSnapshot? currentMarket);
}
