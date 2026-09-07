namespace Trading.Api.ViewModels.Responses;

public class TradingRulesResponse
{
    public int Version { get; set; }
    public decimal MaxNotional { get; set; }
    public decimal MaxQuantity { get; set; }
    public decimal PriceDeviationPercent { get; set; }
    public bool DuplicateIdPreventionEnabled { get; set; }
    public bool SymbolWhitelistEnabled { get; set; }
    public IReadOnlyCollection<string> SymbolWhitelist { get; set; } = Array.Empty<string>();
}
