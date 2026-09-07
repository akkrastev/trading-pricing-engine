using System.Collections.Concurrent;
using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

namespace Trading.TradingEngine.MarketDataProcessing.Services;

public class PriceStateStore : IPriceStateStore
{
    private readonly ConcurrentDictionary<string, SymbolState> _states = new();

    public SymbolState? GetState(string symbol) =>
        _states.TryGetValue(symbol, out var state) ? state : null;

    public void SetState(string symbol, SymbolState state) =>
        _states[symbol] = state;

    public IReadOnlyDictionary<string, SymbolState> GetAllStates() =>
        new Dictionary<string, SymbolState>(_states);

    public void RestoreStates(IReadOnlyDictionary<string, SymbolState> states)
    {
        foreach (var (symbol, state) in states)
        {
            _states[symbol] = state;
        }
    }
}
