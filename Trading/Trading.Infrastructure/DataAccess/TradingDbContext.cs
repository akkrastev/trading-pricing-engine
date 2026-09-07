using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.DataAccess.Entities;

namespace Trading.Infrastructure.DataAccess;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    public DbSet<OrderRejectionReasonEntity> OrderRejectionReasons
        => Set<OrderRejectionReasonEntity>();

    public DbSet<MarketPriceStateEntity> MarketPriceStates
        => Set<MarketPriceStateEntity>();

    public DbSet<TradingRulesEntity> TradingRules
        => Set<TradingRulesEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingDbContext).Assembly);
    }
}
