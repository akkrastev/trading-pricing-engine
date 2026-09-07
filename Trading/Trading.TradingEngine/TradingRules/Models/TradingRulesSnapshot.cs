namespace Trading.TradingEngine.TradingRules.Models;

public class TradingRulesSnapshot
{
    public decimal MaxNotional { get; init; } = 1_000_000m;
    public decimal MaxQuantity { get; init; } = 10_000m;
    public decimal PriceDeviationPercent { get; init; } = 0.8m;
    public bool DuplicateIdPreventionEnabled { get; init; }
    public bool SymbolWhitelistEnabled { get; init; }
    public IReadOnlyCollection<string> SymbolWhitelist { get; init; } = Array.Empty<string>();
    public int Version { get; init; }
}