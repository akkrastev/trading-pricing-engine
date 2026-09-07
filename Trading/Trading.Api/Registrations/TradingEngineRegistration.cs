using Trading.TradingEngine.AutoTrading.Services;
using Trading.TradingEngine.AutoTrading.Services.Interfaces;
using Trading.TradingEngine.MarketDataProcessing.BackgroundServices;
using Trading.TradingEngine.MarketDataProcessing.Services;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;
using Trading.TradingEngine.Orders.Services;
using Trading.TradingEngine.Orders.Services.Interfaces;
using Trading.TradingEngine.TradingRules;
using Trading.TradingEngine.TradingRules.Services;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.TradingEngine;

public static class TradingEngineRegistration
{
    public static IServiceCollection AddTradingEngine(this IServiceCollection services)
    {
        services.AddSingleton<TradingRulesStore>();
        services.AddSingleton<PriceStateStore>();
        services.AddSingleton<IPriceStateStore>(sp => sp.GetRequiredService<PriceStateStore>());

        services.AddSingleton<IPriceMath, PriceMath>();
        services.AddSingleton<ITradingRulesEngine, TradingRulesEngine>();
        services.AddSingleton<IPriceProcessor, PriceProcessor>();
        services.AddSingleton<IOrderProcessor, OrderProcessor>();
        services.AddSingleton<IAutoTradingEngine, AutoTradingEngine>();
        services.AddSingleton<IManualOrderSubmissionService, ManualOrderSubmissionService>();
        services.AddSingleton<ITradingRulesService, TradingRulesService>();

        services.AddHostedService<PriceConsumerHostedService>();

        return services;
    }
}
