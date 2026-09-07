namespace Trading.TradingEngine.MarketDataProcessing.Models;

public class SymbolState
{
    public MarketSnapshot Current { get; init; } = null!;
    public MarketSnapshot? Previous { get; init; }
}
