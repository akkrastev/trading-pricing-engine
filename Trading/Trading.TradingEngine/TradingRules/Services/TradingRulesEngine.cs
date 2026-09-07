using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.TradingRules.Models;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.TradingEngine.TradingRules.Services;

public class TradingRulesEngine : ITradingRulesEngine
{
    public OrderDecision Evaluate(
        OrderRequest request,
        TradingRulesSnapshot rules,
        MarketSnapshot? currentMarket)
    {
        var reasons = new List<RejectionReason>();

        var quantityValid = request.Quantity > 0;
        var priceValid = request.Price > 0;

        if (!quantityValid)
        {
            reasons.Add(new RejectionReason
            {
                Code = ReasonCode.InvalidQuantity,
                Message = "Quantity must be greater than zero.",
            });
        }

        if (!priceValid)
        {
            reasons.Add(new RejectionReason
            {
                Code = ReasonCode.InvalidPrice,
                Message = "Price must be greater than zero.",
            });
        }

        if (quantityValid && request.Quantity > rules.MaxQuantity)
        {
            reasons.Add(new RejectionReason
            {
                Code = ReasonCode.MaxQuantityExceeded,
                Message = $"Order quantity {request.Quantity} exceeds maximum allowed quantity {rules.MaxQuantity}.",
            });
        }

        if (priceValid && quantityValid)
        {
            var notional = request.Price * request.Quantity;

            if (notional > rules.MaxNotional)
            {
                reasons.Add(new RejectionReason
                {
                    Code = ReasonCode.MaxNotionalExceeded,
                    Message = $"Order notional {notional} exceeds maximum allowed notional {rules.MaxNotional}.",
                });
            }
        }

        if (currentMarket is null)
        {
            reasons.Add(new RejectionReason
            {
                Code = ReasonCode.MarketPriceUnavailable,
                Message = $"No current market price is available for symbol {request.Symbol}.",
            });
        }
        else if (priceValid)
        {
            var deviationPercent = Math.Abs(request.Price - currentMarket.CurrentMarketPrice)
                / currentMarket.CurrentMarketPrice * 100;

            if (deviationPercent > rules.PriceDeviationPercent)
            {
                reasons.Add(new RejectionReason
                {
                    Code = ReasonCode.PriceDeviationExceeded,
                    Message = $"Order price {request.Price} deviates {deviationPercent:F2}% from current market price {currentMarket.CurrentMarketPrice}, exceeding the allowed {rules.PriceDeviationPercent}%.",
                });
            }
        }

        if (rules.SymbolWhitelistEnabled &&
            !rules.SymbolWhitelist.Any(s => string.Equals(s, request.Symbol, StringComparison.OrdinalIgnoreCase)))
        {
            reasons.Add(new RejectionReason
            {
                Code = ReasonCode.SymbolNotAllowed,
                Message = $"Symbol {request.Symbol} is not on the trading whitelist.",
            });
        }

        return reasons.Count == 0
            ? new OrderDecision { Status = DecisionStatus.Accepted, Reasons = Array.Empty<RejectionReason>() }
            : new OrderDecision { Status = DecisionStatus.Rejected, Reasons = reasons };
    }
}