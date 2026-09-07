namespace Trading.TradingEngine.Orders.Models;

public class OrderRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Symbol { get; init; } = string.Empty;
    public OrderSide Side { get; init; }
    public decimal Price { get; init; }
    public decimal Quantity { get; init; }
    public OrderSource Source { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}