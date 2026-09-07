using Mapster;
using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.DataAccess.Repositories.Interfaces;
using Trading.Infrastructure.Dtos.Orders;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.DataAccess.Repositories;

public class TradeRequestRepository : ITradeRequestRepository
{
    private const int MaxLimit = 500;

    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public TradeRequestRepository(IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<OrderHistoryDto>> GetFilteredAsync(
        string? symbol,
        DecisionStatus? status,
        DateTime? from,
        DateTime? to,
        int limit,
        CancellationToken cancellationToken = default)
    {
        var clampedLimit = Math.Clamp(limit, 1, MaxLimit);

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var query = db.Orders
            .AsNoTracking()
            .Include(o => o.RejectionReasons)
            .Where(o => o.Source == OrderSource.Manual);

        if (!string.IsNullOrWhiteSpace(symbol))
            query = query.Where(o => o.Symbol == symbol);

        if (status.HasValue)
            query = query.Where(o => o.DecisionStatus == status.Value);

        if (from.HasValue)
            query = query.Where(o => o.Timestamp >= from.Value);

        if (to.HasValue)
            query = query.Where(o => o.Timestamp <= to.Value);

        var entities = await query
            .OrderByDescending(o => o.Timestamp)
            .Take(clampedLimit)
            .ToListAsync(cancellationToken);

        return entities.Adapt<List<OrderHistoryDto>>();
    }
}