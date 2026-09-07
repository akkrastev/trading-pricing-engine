using Mapster;
using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.DataAccess.Entities;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.Orders.Services.Interfaces;

namespace Trading.Infrastructure.Services.Order;

public class OrderSubmissionPersistence : IOrderSubmissionPersistence
{
    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public OrderSubmissionPersistence(IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> ExistsAsync(Guid orderId)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return await dbContext.Orders.AnyAsync(o => o.OrderId == orderId);
    }

    public async Task SaveAsync(ProcessedOrder processedOrder)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        var entity = processedOrder.Adapt<OrderEntity>();
        entity.RejectionReasons = processedOrder.Decision.Reasons
            .Adapt<List<OrderRejectionReasonEntity>>();

        dbContext.Orders.Add(entity);
        await dbContext.SaveChangesAsync();
    }
}