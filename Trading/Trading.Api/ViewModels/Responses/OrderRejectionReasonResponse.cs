using Trading.TradingEngine.Orders.Models;

namespace Trading.Api.ViewModels.Responses;

public class OrderRejectionReasonResponse
{
    public ReasonCode Code { get; set; }
    public string Message { get; set; } = string.Empty;
}
