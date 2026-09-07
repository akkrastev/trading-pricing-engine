using Trading.TradingEngine.TradingRules.Models;

namespace Trading.TradingEngine.TradingRules.Services.Interfaces;

public interface ITradingRulesService
{
    TradingRulesSnapshot GetCurrent();

    Task<TradingRulesSnapshot> UpdateAsync(
        decimal maxNotional,
        decimal maxQuantity,
        decimal priceDeviationPercent,
        bool duplicateIdPreventionEnabled,
        bool symbolWhitelistEnabled,
        IReadOnlyCollection<string> symbolWhitelist,
        CancellationToken cancellationToken = default);
}
