using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.TradingRules.Models;

namespace Trading.TradingEngine.Orders.Services.Interfaces;

public interface IOrderProcessor
{
    ProcessedOrder Process(OrderRequest request);
    ProcessedOrder Process(OrderRequest request, TradingRulesSnapshot rules);
}
