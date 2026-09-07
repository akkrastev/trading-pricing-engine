using Mapster;
using Trading.Infrastructure.DataAccess.Entities;
using Trading.Infrastructure.Dtos.Orders;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Infrastructure.Mapping;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<ProcessedOrder, OrderEntity>.NewConfig()
            .Map(dest => dest.OrderId, src => src.Request.Id)
            .Map(dest => dest.Symbol, src => src.Request.Symbol)
            .Map(dest => dest.Side, src => src.Request.Side)
            .Map(dest => dest.Price, src => src.Request.Price)
            .Map(dest => dest.Quantity, src => src.Request.Quantity)
            .Map(dest => dest.Source, src => src.Request.Source)
            .Map(dest => dest.Timestamp, src => src.Request.Timestamp)
            .Map(dest => dest.DecisionStatus, src => src.Decision.Status)
            .Map(dest => dest.RulesVersion, src => src.RulesVersion)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.RejectionReasons);

        TypeAdapterConfig<RejectionReason, OrderRejectionReasonEntity>.NewConfig()
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Message, src => src.Message)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.OrderEntityId);

        TypeAdapterConfig<OrderEntity, OrderHistoryDto>.NewConfig()
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

        TypeAdapterConfig<OrderRejectionReasonEntity, OrderRejectionReasonDto>.NewConfig()
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Message, src => src.Message);
    }
}
