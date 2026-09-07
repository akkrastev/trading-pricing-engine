using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.Dtos.Orders;

public class OrderHistoryDto
{
    public Guid OrderId { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public OrderSide Side { get; init; }
    public decimal Price { get; init; }
    public decimal Quantity { get; init; }
    public OrderSource Source { get; init; }
    public DateTime Timestamp { get; init; }
    public DecisionStatus DecisionStatus { get; init; }
    public int RulesVersion { get; init; }
    public IReadOnlyList<OrderRejectionReasonDto> RejectionReasons { get; init; }
        = Array.Empty<OrderRejectionReasonDto>();
}