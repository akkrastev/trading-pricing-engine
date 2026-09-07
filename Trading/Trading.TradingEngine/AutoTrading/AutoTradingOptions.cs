namespace Trading.TradingEngine.AutoTrading;

public class AutoTradingOptions
{
    public decimal SpreadPercentThreshold { get; init; } = 0.02m;
    public decimal Quantity { get; init; } = 100m;
}