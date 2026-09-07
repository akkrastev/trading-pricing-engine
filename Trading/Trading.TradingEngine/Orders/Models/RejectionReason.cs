namespace Trading.TradingEngine.Orders.Models;

public enum ReasonCode
{
    MaxQuantityExceeded,
    MaxNotionalExceeded,
    PriceDeviationExceeded,
    SymbolNotAllowed,
    DuplicateOrderId,
    MarketPriceUnavailable,
    InvalidQuantity,
    InvalidPrice
}

public class RejectionReason
{
    public ReasonCode Code { get; init; }
    public string Message { get; init; } = string.Empty;
}