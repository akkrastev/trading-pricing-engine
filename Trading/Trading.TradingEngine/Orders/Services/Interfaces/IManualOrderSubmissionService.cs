using Trading.TradingEngine.Orders.Models;

namespace Trading.TradingEngine.Orders.Services.Interfaces;

public interface IManualOrderSubmissionService
{
    Task<ProcessedOrder> SubmitAsync(OrderRequest request);
}
