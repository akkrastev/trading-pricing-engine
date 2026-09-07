using Trading.Infrastructure.Dtos.Orders;

namespace Trading.Infrastructure.DataAccess.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderHistoryDto>> GetBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default);
}