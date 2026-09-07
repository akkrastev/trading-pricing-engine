namespace Trading.Infrastructure.DataAccess.Entities;

public class TradingRulesEntity
{
    public int Version { get; set; }

    public decimal MaxNotional { get; set; }

    public decimal MaxQuantity { get; set; }

    public decimal PriceDeviationPercent { get; set; }

    public bool DuplicateIdPreventionEnabled { get; set; }

    public bool SymbolWhitelistEnabled { get; set; }

    public string SymbolWhitelistJson { get; set; } = "[]";

    public DateTime CreatedAtUtc { get; set; }
}
