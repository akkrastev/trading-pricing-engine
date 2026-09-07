using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.DataAccess.Entities;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.BackgroundServices;


public class OrderPersistenceWorker : BackgroundService
{
    private readonly ChannelReader<ProcessedOrder> _reader;
    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public OrderPersistenceWorker(
        ChannelReader<ProcessedOrder> reader,
        IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _reader = reader;
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var processedOrder in _reader.ReadAllAsync(stoppingToken))
        {
            await using var dbContext =
                await _dbContextFactory.CreateDbContextAsync(stoppingToken);

            var entity = processedOrder.Adapt<OrderEntity>();
            entity.RejectionReasons =
                processedOrder.Decision.Reasons
                    .Adapt<List<OrderRejectionReasonEntity>>();

            dbContext.Orders.Add(entity);
            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}