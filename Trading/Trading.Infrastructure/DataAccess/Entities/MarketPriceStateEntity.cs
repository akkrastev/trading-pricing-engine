namespace Trading.Infrastructure.DataAccess.Entities;

public class MarketPriceStateEntity
{
    public string Symbol { get; set; } = string.Empty;

    public decimal CurrentBidPrice { get; set; }
    public decimal CurrentAskPrice { get; set; }
    public decimal CurrentMarketPrice { get; set; }
    public decimal CurrentSpread { get; set; }
    public decimal CurrentSpreadPercent { get; set; }
    public DateTime CurrentTimestamp { get; set; }

    public decimal? PreviousBidPrice { get; set; }
    public decimal? PreviousAskPrice { get; set; }
    public decimal? PreviousMarketPrice { get; set; }
    public decimal? PreviousSpread { get; set; }
    public decimal? PreviousSpreadPercent { get; set; }
    public DateTime? PreviousTimestamp { get; set; }
}
