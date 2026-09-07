using Trading.TradingEngine.TradingRules.Models;

namespace Trading.TradingEngine.TradingRules.Services.Interfaces;

public interface ITradingRulesPersistence
{
    Task PersistAsync(TradingRulesSnapshot snapshot, CancellationToken cancellationToken = default);

    Task<TradingRulesSnapshot?> LoadLatestAsync(CancellationToken cancellationToken = default);
}