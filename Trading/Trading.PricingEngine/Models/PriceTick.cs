namespace Trading.PricingEngine.Models
{
    public class PriceTick
    {
        public string Symbol { get; init; } = string.Empty;
        public decimal BidPrice { get; init; }
        public decimal AskPrice { get; init; }
        public DateTime Timestamp { get; init; }
    }
}
