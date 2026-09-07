using Trading.TradingEngine.MarketDataProcessing.Models;

namespace Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

public interface IPriceStateStore
{
    SymbolState? GetState(string symbol);
    void SetState(string symbol, SymbolState state);
    IReadOnlyDictionary<string, SymbolState> GetAllStates();
    void RestoreStates(IReadOnlyDictionary<string, SymbolState> states);
}
