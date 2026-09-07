namespace Trading.PricingEngine.MarketDataSimulation.Data;

public static class SymbolBasePrices
{
    public static readonly Dictionary<string, decimal> StartingPrices = new Dictionary<string, decimal>
    {
        ["AAPL"] = 227.50m,
        ["MSFT"] = 415.20m,
        ["GOOGL"] = 168.90m,
        ["AMZN"] = 186.40m,
        ["TSLA"] = 248.30m,
        ["META"] = 512.75m,
        ["NVDA"] = 124.60m,
        ["NFLX"] = 692.10m,
        ["AMD"] = 158.20m,
        ["INTC"] = 23.45m,

        ["EURUSD"] = 1.08m,
        ["GBPUSD"] = 1.27m,
        ["BTCUSD"] = 65000.00m,
        ["ETHUSD"] = 3200.00m,
    };
    
}