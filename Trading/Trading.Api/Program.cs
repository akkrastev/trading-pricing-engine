using Scalar.AspNetCore;
using Trading.Api.Mapping;
using Trading.Api.Registrations;
using Trading.Infrastructure;
using Trading.Infrastructure.DataAccess;
using Trading.Infrastructure.Services.Interfaces;
using Trading.PricingEngine;
using Trading.PricingEngine.Configuration;
using Trading.TradingEngine;
using Trading.TradingEngine.AutoTrading;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;
using Trading.TradingEngine.TradingRules;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<PricingOptions>(
    builder.Configuration.GetSection("Pricing"));

builder.Services.Configure<AutoTradingOptions>(
    builder.Configuration.GetSection("AutoTrading")); 

builder.Services.AddPriceChannel();
builder.Services.AddOrderChannel();
builder.Services.AddPricingEngine();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddTradingEngine();

MappingConfig.Configure();

var app = builder.Build();

await app.Services.MigrateDatabaseAsync();

using (var scope = app.Services.CreateScope())
{
    var priceStatePersistence = scope.ServiceProvider.GetRequiredService<IPriceStatePersistence>();
    var priceStateStore = scope.ServiceProvider.GetRequiredService<IPriceStateStore>();

    var restoredStates = await priceStatePersistence.LoadAllAsync();
    priceStateStore.RestoreStates(restoredStates);

    var tradingRulesPersistence = scope.ServiceProvider.GetRequiredService<ITradingRulesPersistence>();
    var tradingRulesStore = scope.ServiceProvider.GetRequiredService<TradingRulesStore>();

    var restoredRules = await tradingRulesPersistence.LoadLatestAsync();
    if (restoredRules is not null)
    {
        tradingRulesStore.SetCurrent(restoredRules);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
