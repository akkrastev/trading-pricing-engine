using Trading.TradingEngine.Orders.Models;

namespace Trading.Api.ViewModels.Responses;

public class ProcessedOrderResponse
{
    public Guid OrderId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public OrderSide Side { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public OrderSource Source { get; set; }
    public DateTime Timestamp { get; set; }
    public DecisionStatus DecisionStatus { get; set; }
    public int RulesVersion { get; set; }
    public IReadOnlyList<OrderRejectionReasonResponse> RejectionReasons { get; set; } = Array.Empty<OrderRejectionReasonResponse>();
}