using Microsoft.Extensions.Hosting;
using Trading.Infrastructure.Services.Interfaces;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

namespace Trading.Infrastructure.BackgroundServices;

public class PriceStatePersistenceWorker : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(10);
    private static readonly TimeSpan FinalFlushTimeout = TimeSpan.FromSeconds(5);

    private readonly IPriceStateStore _store;
    private readonly IPriceStatePersistence _persistence;

    public PriceStatePersistenceWorker(
        IPriceStateStore store,
        IPriceStatePersistence persistence)
    {
        _store = store;
        _persistence = persistence;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (true)
            {
                await Task.Delay(Interval, stoppingToken);
                await PersistSafelyAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {

        }
        finally
        {
            using var finalFlushCts = new CancellationTokenSource(FinalFlushTimeout);
            await PersistSafelyAsync(finalFlushCts.Token);
        }
    }

    private async Task PersistSafelyAsync(CancellationToken cancellationToken)
    {
        try
        {
            var snapshot = _store.GetAllStates();
            await _persistence.PersistAllAsync(snapshot, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {

        }
        catch (Exception ex)
        {

        }
    }
}
