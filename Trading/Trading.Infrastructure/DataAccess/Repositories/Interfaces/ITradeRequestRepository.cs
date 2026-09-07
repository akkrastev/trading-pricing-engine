using Trading.Infrastructure.Dtos.Orders;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.DataAccess.Repositories.Interfaces;

public interface ITradeRequestRepository
{
    Task<List<OrderHistoryDto>> GetFilteredAsync(
        string? symbol,
        DecisionStatus? status,
        DateTime? from,
        DateTime? to,
        int limit,
        CancellationToken cancellationToken = default);
}