using System.ComponentModel.DataAnnotations;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Api.ViewModels.Requests;

public class SubmitTradeRequest
{
    public Guid OrderId { get; set; }

    [Required]
    public string Symbol { get; set; } = string.Empty;

    public OrderSide Side { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }
}