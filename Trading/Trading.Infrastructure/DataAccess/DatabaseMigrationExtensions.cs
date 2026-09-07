using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Trading.Infrastructure.DataAccess;

public static class DatabaseMigrationExtensions
{
    public static async Task MigrateDatabaseAsync(this IServiceProvider services)
    {
        var dbContextFactory = services.GetRequiredService<IDbContextFactory<TradingDbContext>>();

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        await dbContext.Database.MigrateAsync();
    }
}