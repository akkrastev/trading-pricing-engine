using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.Orders.Services.Interfaces;
using Trading.TradingEngine.TradingRules;

namespace Trading.TradingEngine.Orders.Services;

public class ManualOrderSubmissionService : IManualOrderSubmissionService
{
    private readonly IOrderProcessor _orderProcessor;
    private readonly TradingRulesStore _tradingRulesStore;
    private readonly IOrderSubmissionPersistence _persistence;
    private readonly SemaphoreSlim _duplicateCheckSemaphore = new(1, 1);

    public ManualOrderSubmissionService(
        IOrderProcessor orderProcessor,
        TradingRulesStore tradingRulesStore,
        IOrderSubmissionPersistence persistence)
    {
        _orderProcessor = orderProcessor;
        _tradingRulesStore = tradingRulesStore;
        _persistence = persistence;
    }

    public async Task<ProcessedOrder> SubmitAsync(OrderRequest request)
    {
        var snapshot = _tradingRulesStore.GetCurrent();
        var baseline = _orderProcessor.Process(request, snapshot);

        if (!snapshot.DuplicateIdPreventionEnabled)
        {
            var processedOrder = new ProcessedOrder
            {
                Request = request,
                Decision = baseline.Decision,
                RulesVersion = snapshot.Version,
            };

            await _persistence.SaveAsync(processedOrder);
            return processedOrder;
        }

        await _duplicateCheckSemaphore.WaitAsync();
        try
        {
            var isDuplicate = await _persistence.ExistsAsync(request.Id);

            var decision = isDuplicate
                ? AppendDuplicateReason(baseline.Decision)
                : baseline.Decision;

            var processedOrder = new ProcessedOrder
            {
                Request = request,
                Decision = decision,
                RulesVersion = snapshot.Version,
            };

            await _persistence.SaveAsync(processedOrder);
            return processedOrder;
        }
        finally
        {
            _duplicateCheckSemaphore.Release();
        }
    }

    private static OrderDecision AppendDuplicateReason(OrderDecision baseline)
    {
        var reasons = new List<RejectionReason>(baseline.Reasons)
        {
            new()
            {
                Code = ReasonCode.DuplicateOrderId,
                Message = "An order with this ID has already been submitted.",
            },
        };

        return new OrderDecision
        {
            Status = DecisionStatus.Rejected,
            Reasons = reasons,
        };
    }
}