using Trading.PricingEngine.Models;
using Trading.TradingEngine.MarketDataProcessing.Models;

namespace Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

public interface IPriceProcessor
{
    SymbolState Process(PriceTick tick);
}
