using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

public interface IPriceMath
{
    decimal CalculateMidPrice(decimal bidPrice, decimal askPrice);
    decimal CalculateSpread(decimal bidPrice, decimal askPrice);
    decimal CalculateSpreadPercent(decimal spread, decimal midPrice);
}
