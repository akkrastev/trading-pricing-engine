using Trading.PricingEngine.Models;

namespace Trading.PricingEngine.Services.Interfaces;

public interface IPriceGenerator
{
    (decimal NextPrice, PriceTick Tick) Generate(string symbol, decimal currentPrice);
}