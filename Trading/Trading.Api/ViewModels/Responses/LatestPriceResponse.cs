namespace Trading.Api.ViewModels.Responses;

public class LatestPriceResponse
{
    public string Symbol { get; set; } = string.Empty;
    public decimal BidPrice { get; set; }
    public decimal AskPrice { get; set; }
    public decimal CurrentMarketPrice { get; set; }
    public decimal Spread { get; set; }
    public decimal SpreadPercent { get; set; }
    public DateTime Timestamp { get; set; }
}