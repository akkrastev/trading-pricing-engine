using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.Dtos.Orders;

public class OrderRejectionReasonDto
{
    public ReasonCode Code { get; init; }
    public string Message { get; init; } = string.Empty;
}
