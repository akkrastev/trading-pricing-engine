namespace Trading.PricingEngine.Configuration;

public class PricingOptions
{
    public const string SectionName = "Pricing";

    public string[] Symbols { get; set; } =
    [
        "AAPL",
        "GOOGL",
        "MSFT",
        "AMZN",
        "TSLA",
        "NVDA",
        "EURUSD",
        "GBPUSD",
        "BTCUSD",
        "ETHUSD"
    ];

    public int TickIntervalMs { get; set; } = 500;

    // Simulation parameters. Not assignment requirements — implementation choices
    // that make the generated feed look plausible.
    public decimal MaxPriceMovePercent { get; set; } = 0.1m;
    public decimal MinSpreadPercent { get; set; } = 0.01m;
    public decimal MaxSpreadPercent { get; set; } = 0.05m;
}