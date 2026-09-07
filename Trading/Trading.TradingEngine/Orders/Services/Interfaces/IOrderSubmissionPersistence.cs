using Trading.TradingEngine.Orders.Models;

namespace Trading.TradingEngine.Orders.Services.Interfaces;

public interface IOrderSubmissionPersistence
{
    Task<bool> ExistsAsync(Guid orderId);

    Task SaveAsync(ProcessedOrder processedOrder);
}
