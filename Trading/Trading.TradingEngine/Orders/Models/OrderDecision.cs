namespace Trading.TradingEngine.Orders.Models;

public enum DecisionStatus
{
    Accepted,
    Rejected
}

public class OrderDecision
{
    public DecisionStatus Status { get; init; }
    public IReadOnlyList<RejectionReason> Reasons { get; init; } = Array.Empty<RejectionReason>();
}