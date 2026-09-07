using Trading.TradingEngine.MarketDataProcessing.Models;

namespace Trading.Infrastructure.Services.Interfaces;

public interface IPriceStatePersistence
{
    Task PersistAllAsync(IReadOnlyDictionary<string, SymbolState> states, CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, SymbolState>> LoadAllAsync(CancellationToken cancellationToken = default);
}