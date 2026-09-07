using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

namespace Trading.TradingEngine.MarketDataProcessing.Services;

public class PriceMath : IPriceMath
{
    public decimal CalculateMidPrice(decimal bidPrice, decimal askPrice) =>
        (bidPrice + askPrice) / 2;

    public decimal CalculateSpread(decimal bidPrice, decimal askPrice) =>
        askPrice - bidPrice;

    public decimal CalculateSpreadPercent(decimal spread, decimal midPrice) =>
        spread / midPrice * 100;
}
