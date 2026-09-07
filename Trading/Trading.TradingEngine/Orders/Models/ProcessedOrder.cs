namespace Trading.TradingEngine.Orders.Models;

public class ProcessedOrder
{
    public OrderRequest Request { get; init; } = default!;
    public OrderDecision Decision { get; init; } = default!;
    public int RulesVersion { get; init; }
}
