using Trading.PricingEngine.MarketDataSimulation.BackgroundServices;
using Trading.PricingEngine.Services;
using Trading.PricingEngine.Services.Interfaces;

namespace Trading.PricingEngine;

public static class PricingEngineRegistration
{
    public static IServiceCollection AddPricingEngine(this IServiceCollection services)
    {
        services.AddSingleton<IPriceGenerator, PriceGenerator>();
        services.AddSingleton<IPriceTickValidator, PriceTickValidator>();
        services.AddHostedService<MarketDataGenerator>();

        return services;
    }
}
