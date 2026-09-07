using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.DataAccess.Entities;
using Trading.Infrastructure.Services.Interfaces;
using Trading.TradingEngine.MarketDataProcessing.Models;

namespace Trading.Infrastructure.Services.PriceState;

public class PriceStatePersistence : IPriceStatePersistence
{
    private readonly IDbContextFactory<TradingDbContext> _dbContextFactory;

    public PriceStatePersistence(IDbContextFactory<TradingDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task PersistAllAsync(IReadOnlyDictionary<string, SymbolState> states, CancellationToken cancellationToken = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var existing = await db.MarketPriceStates.ToDictionaryAsync(x => x.Symbol, cancellationToken);

        foreach (var (symbol, state) in states)
        {
            if (!existing.TryGetValue(symbol, out var entity))
            {
                entity = new MarketPriceStateEntity { Symbol = symbol };
                db.MarketPriceStates.Add(entity);
            }

            entity.CurrentBidPrice = state.Current.BidPrice;
            entity.CurrentAskPrice = state.Current.AskPrice;
            entity.CurrentMarketPrice = state.Current.CurrentMarketPrice;
            entity.CurrentSpread = state.Current.Spread;
            entity.CurrentSpreadPercent = state.Current.SpreadPercent;
            entity.CurrentTimestamp = state.Current.Timestamp;

            entity.PreviousBidPrice = state.Previous?.BidPrice;
            entity.PreviousAskPrice = state.Previous?.AskPrice;
            entity.PreviousMarketPrice = state.Previous?.CurrentMarketPrice;
            entity.PreviousSpread = state.Previous?.Spread;
            entity.PreviousSpreadPercent = state.Previous?.SpreadPercent;
            entity.PreviousTimestamp = state.Previous?.Timestamp;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyDictionary<string, SymbolState>> LoadAllAsync(CancellationToken cancellationToken = default)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entities = await db.MarketPriceStates.AsNoTracking().ToListAsync(cancellationToken);

        var result = new Dictionary<string, SymbolState>();

        foreach (var entity in entities)
        {
            var current = new MarketSnapshot
            {
                BidPrice = entity.CurrentBidPrice,
                AskPrice = entity.CurrentAskPrice,
                CurrentMarketPrice = entity.CurrentMarketPrice,
                Spread = entity.CurrentSpread,
                SpreadPercent = entity.CurrentSpreadPercent,
                Timestamp = entity.CurrentTimestamp,    
            };

            MarketSnapshot? previous = entity.PreviousMarketPrice is null
                ? null
                : new MarketSnapshot
                {
                    BidPrice = entity.PreviousBidPrice!.Value,
                    AskPrice = entity.PreviousAskPrice!.Value,
                    CurrentMarketPrice = entity.PreviousMarketPrice!.Value,
                    Spread = entity.PreviousSpread!.Value,
                    SpreadPercent = entity.PreviousSpreadPercent!.Value,
                    Timestamp = entity.PreviousTimestamp!.Value,
                };

            result[entity.Symbol] = new SymbolState { Current = current, Previous = previous };
        }

        return result;
    }
}
