using Trading.TradingEngine.TradingRules.Models;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.TradingEngine.TradingRules.Services;

public class TradingRulesService : ITradingRulesService
{
    private readonly TradingRulesStore _store;
    private readonly ITradingRulesPersistence _persistence;
    private readonly SemaphoreSlim _updateSemaphore = new(1, 1);

    public TradingRulesService(TradingRulesStore store, ITradingRulesPersistence persistence)
    {
        _store = store;
        _persistence = persistence;
    }

    public TradingRulesSnapshot GetCurrent() => _store.GetCurrent();

    public async Task<TradingRulesSnapshot> UpdateAsync(
        decimal maxNotional,
        decimal maxQuantity,
        decimal priceDeviationPercent,
        bool duplicateIdPreventionEnabled,
        bool symbolWhitelistEnabled,
        IReadOnlyCollection<string> symbolWhitelist,
        CancellationToken cancellationToken = default)
    {
        await _updateSemaphore.WaitAsync(cancellationToken);
        try
        {
            var current = _store.GetCurrent();

            var next = new TradingRulesSnapshot
            {
                MaxNotional = maxNotional,
                MaxQuantity = maxQuantity,
                PriceDeviationPercent = priceDeviationPercent,
                DuplicateIdPreventionEnabled = duplicateIdPreventionEnabled,
                SymbolWhitelistEnabled = symbolWhitelistEnabled,
                SymbolWhitelist = symbolWhitelist.ToArray(),
                Version = current.Version + 1,
            };

            await _persistence.PersistAsync(next, cancellationToken);

            _store.SetCurrent(next);

            return next;
        }
        finally
        {
            _updateSemaphore.Release();
        }
    }
}