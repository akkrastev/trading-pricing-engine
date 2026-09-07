using Trading.TradingEngine.TradingRules.Models;

namespace Trading.TradingEngine.TradingRules;

public class TradingRulesStore
{
    private readonly object _updateLock = new();
    private volatile TradingRulesSnapshot _current = new();

    public TradingRulesSnapshot GetCurrent() => _current;

    public void SetCurrent(TradingRulesSnapshot snapshot)
    {
        lock (_updateLock)
        {
            _current = snapshot;
        }
    }
}