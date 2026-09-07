using Trading.PricingEngine.Models;
using Trading.PricingEngine.Services.Interfaces;

namespace Trading.PricingEngine.Services;

public class PriceTickValidator : IPriceTickValidator
{
    public bool IsValid(PriceTick tick)
    {
        return tick.BidPrice > 0
            && tick.AskPrice > 0
            && tick.BidPrice < tick.AskPrice
            && !string.IsNullOrWhiteSpace(tick.Symbol);
    }
}