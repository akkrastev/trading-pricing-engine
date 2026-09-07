using Mapster;
using Trading.Api.ViewModels.Requests;
using Trading.Api.ViewModels.Responses;
using Trading.Infrastructure.Dtos.Orders;
using Trading.TradingEngine.MarketDataProcessing.Models;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.TradingRules.Models;

namespace Trading.Api.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<SubmitTradeRequest, OrderRequest>.NewConfig()
            .Map(dest => dest.Id, src => src.OrderId)
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Side, src => src.Side)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Source, src => OrderSource.Manual)
            .Map(dest => dest.Timestamp, src => DateTime.UtcNow);

        TypeAdapterConfig<ProcessedOrder, ProcessedOrderResponse>.NewConfig()
            .Map(dest => dest.OrderId, src => src.Request.Id)
            .Map(dest => dest.Symbol, src => src.Request.Symbol)
            .Map(dest => dest.Side, src => src.Request.Side)
            .Map(dest => dest.Price, src => src.Request.Price)
            .Map(dest => dest.Quantity, src => src.Request.Quantity)
            .Map(dest => dest.Source, src => src.Request.Source)
            .Map(dest => dest.Timestamp, src => src.Request.Timestamp)
            .Map(dest => dest.DecisionStatus, src => src.Decision.Status)
            .Map(dest => dest.RejectionReasons, src => src.Decision.Reasons)
            .Map(dest => dest.RulesVersion, src => src.RulesVersion);

        TypeAdapterConfig<RejectionReason, OrderRejectionReasonResponse>.NewConfig()
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Message, src => src.Message);

        TypeAdapterConfig<OrderHistoryDto, OrderHistoryItemResponse>.NewConfig()
            .Map(dest => dest.OrderId, src => src.OrderId)
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.Side, src => src.Side)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Source, src => src.Source)
            .Map(dest => dest.Timestamp, src => src.Timestamp)
            .Map(dest => dest.DecisionStatus, src => src.DecisionStatus)
            .Map(dest => dest.RulesVersion, src => src.RulesVersion)
            .Map(dest => dest.RejectionReasons, src => src.RejectionReasons);

        TypeAdapterConfig<OrderRejectionReasonDto, OrderRejectionReasonResponse>.NewConfig()
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Message, src => src.Message);

        TypeAdapterConfig<TradingRulesSnapshot, TradingRulesResponse>.NewConfig()
            .Map(dest => dest.MaxNotional, src => src.MaxNotional)
            .Map(dest => dest.MaxQuantity, src => src.MaxQuantity)
            .Map(dest => dest.PriceDeviationPercent, src => src.PriceDeviationPercent)
            .Map(dest => dest.DuplicateIdPreventionEnabled, src => src.DuplicateIdPreventionEnabled)
            .Map(dest => dest.SymbolWhitelistEnabled, src => src.SymbolWhitelistEnabled)
            .Map(dest => dest.SymbolWhitelist, src => src.SymbolWhitelist)
            .Map(dest => dest.Version, src => src.Version);

        TypeAdapterConfig<MarketSnapshot, LatestPriceResponse>.NewConfig()
            .Map(dest => dest.Symbol, src => src.Symbol)
            .Map(dest => dest.BidPrice, src => src.BidPrice)
            .Map(dest => dest.AskPrice, src => src.AskPrice)
            .Map(dest => dest.CurrentMarketPrice, src => src.CurrentMarketPrice)
            .Map(dest => dest.Spread, src => src.Spread)
            .Map(dest => dest.SpreadPercent, src => src.SpreadPercent)
            .Map(dest => dest.Timestamp, src => src.Timestamp);
    }
}
