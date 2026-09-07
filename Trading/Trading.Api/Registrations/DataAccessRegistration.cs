using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.BackgroundServices;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.DataAccess.Repositories;
using Trading.Infrastructure.DataAccess.Repositories.Interfaces;
using Trading.Infrastructure.Mapping;
using Trading.Infrastructure.Services.Interfaces;
using Trading.Infrastructure.Services.Order;
using Trading.Infrastructure.Services.PriceState;
using Trading.Infrastructure.Services.TradingRules;
using Trading.TradingEngine.Orders.Services.Interfaces;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.Infrastructure;

public static class DataAccessRegistration
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TradingDatabase")
            ?? throw new InvalidOperationException(
                "Connection string 'TradingDatabase' was not found in configuration.");

        services.AddDbContextFactory<TradingDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddSingleton<IOrderSubmissionPersistence, OrderSubmissionPersistence>();
        services.AddSingleton<IPriceStatePersistence, PriceStatePersistence>();
        services.AddSingleton<ITradingRulesPersistence, TradingRulesPersistence>();

        services.AddSingleton<ITradeRequestRepository, TradeRequestRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();

        services.AddHostedService<OrderPersistenceWorker>();
        services.AddHostedService<PriceStatePersistenceWorker>();

        MappingConfig.Configure();

        return services;
    }
}