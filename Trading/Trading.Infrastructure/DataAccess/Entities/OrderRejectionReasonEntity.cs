using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.DataAccess.Entities;

public class OrderRejectionReasonEntity
{
    public long Id { get; set; }

    public long OrderEntityId { get; set; }

    public ReasonCode Code { get; set; }

    public string Message { get; set; } = string.Empty;
}
