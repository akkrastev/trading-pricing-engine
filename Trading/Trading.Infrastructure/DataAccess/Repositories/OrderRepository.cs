using Mapster;
using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.DataAccess.Repositories.Interfaces;
using Trading.Infrastructure.Dtos.Orders;

namespace Trading.Infrastructure.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private const int MaxResultCount = 500;

    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public OrderRepository(IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<OrderHistoryDto>> GetBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entities = await db.Orders
            .AsNoTracking()
            .Include(o => o.RejectionReasons)
            .Where(o => o.Symbol == symbol)
            .OrderByDescending(o => o.Timestamp)
            .Take(MaxResultCount)
            .ToListAsync(cancellationToken);

        return entities.Adapt<List<OrderHistoryDto>>();
    }
}