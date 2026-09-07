namespace Trading.TradingEngine.MarketDataProcessing.Models;

public class MarketSnapshot
{
    public string Symbol { get; init; } = string.Empty;
    public decimal BidPrice { get; init; }
    public decimal AskPrice { get; init; }
    public decimal CurrentMarketPrice { get; init; }
    public decimal Spread { get; init; }
    public decimal SpreadPercent { get; init; }
    public DateTime Timestamp { get; init; }
}
