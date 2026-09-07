using Trading.TradingEngine.Orders.Models;

namespace Trading.Api.ViewModels.Requests;

public class TradeRequestHistoryFilter
{
    public string? Symbol { get; set; }
    public DecisionStatus? Status { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Limit { get; set; } = 100;
}