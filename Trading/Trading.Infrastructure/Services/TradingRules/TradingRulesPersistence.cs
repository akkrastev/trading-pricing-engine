using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.DataAccess.Entities;
using Trading.TradingEngine.TradingRules.Models;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.Infrastructure.Services.TradingRules;

public class TradingRulesPersistence : ITradingRulesPersistence
{
    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public TradingRulesPersistence(IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task PersistAsync(TradingRulesSnapshot snapshot, CancellationToken cancellationToken = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = new TradingRulesEntity
        {
            Version = snapshot.Version,
            MaxNotional = snapshot.MaxNotional,
            MaxQuantity = snapshot.MaxQuantity,
            PriceDeviationPercent = snapshot.PriceDeviationPercent,
            DuplicateIdPreventionEnabled = snapshot.DuplicateIdPreventionEnabled,
            SymbolWhitelistEnabled = snapshot.SymbolWhitelistEnabled,
            SymbolWhitelistJson = JsonSerializer.Serialize(snapshot.SymbolWhitelist),
            CreatedAtUtc = DateTime.UtcNow,
        };

        db.TradingRules.Add(entity);

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<TradingRulesSnapshot?> LoadLatestAsync(CancellationToken cancellationToken = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.TradingRules
            .AsNoTracking()
            .OrderByDescending(x => x.Version)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return null;

        var symbolWhitelist = JsonSerializer.Deserialize<string[]>(entity.SymbolWhitelistJson)
            ?? Array.Empty<string>();

        return new TradingRulesSnapshot
        {
            Version = entity.Version,
            MaxNotional = entity.MaxNotional,
            MaxQuantity = entity.MaxQuantity,
            PriceDeviationPercent = entity.PriceDeviationPercent,
            DuplicateIdPreventionEnabled = entity.DuplicateIdPreventionEnabled,
            SymbolWhitelistEnabled = entity.SymbolWhitelistEnabled,
            SymbolWhitelist = symbolWhitelist,
        };
    }
}
