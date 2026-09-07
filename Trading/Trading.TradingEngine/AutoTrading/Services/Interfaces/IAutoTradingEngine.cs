using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.Orders.Models;

namespace Trading.TradingEngine.AutoTrading.Services.Interfaces;

public interface IAutoTradingEngine
{
    ProcessedOrder? Process(SymbolState state);
}