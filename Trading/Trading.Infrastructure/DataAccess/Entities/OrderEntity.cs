using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.DataAccess.Entities;

public class OrderEntity
{
    public long Id { get; set; }

    public Guid OrderId { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public OrderSide Side { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public OrderSource Source { get; set; }

    public DateTime Timestamp { get; set; }

    public DecisionStatus DecisionStatus { get; set; }

    public int RulesVersion { get; set; }

    public ICollection<OrderRejectionReasonEntity> RejectionReasons { get; set; }
        = new List<OrderRejectionReasonEntity>();
}
