using Mapster;
using Microsoft.AspNetCore.Mvc;
using Trading.Api.ViewModels.Requests;
using Trading.Api.ViewModels.Responses;
using Trading.Infrastructure.DataAccess.Repositories.Interfaces;
using Trading.TradingEngine.Orders.Models;
using Trading.TradingEngine.Orders.Services.Interfaces;

namespace Trading.Api.Controllers;

[ApiController]
[Route("api/trade-requests")]
public class TradeRequestsController : ControllerBase
{
    private readonly IManualOrderSubmissionService _manualOrderSubmissionService;
    private readonly ITradeRequestRepository _tradeRequestRepository;

    public TradeRequestsController(
        IManualOrderSubmissionService manualOrderSubmissionService,
        ITradeRequestRepository tradeRequestRepository)
    {
        _manualOrderSubmissionService = manualOrderSubmissionService;
        _tradeRequestRepository = tradeRequestRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitTradeRequest request)
    {
        var orderRequest = request.Adapt<OrderRequest>();
        var processed = await _manualOrderSubmissionService.SubmitAsync(orderRequest);
        var response = processed.Adapt<ProcessedOrderResponse>();

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetHistory(
        [FromQuery] TradeRequestHistoryFilter filter,
        CancellationToken cancellationToken)
    {
        var history = await _tradeRequestRepository.GetFilteredAsync(
            filter.Symbol,
            filter.Status,
            filter.From,
            filter.To,
            filter.Limit,
            cancellationToken);

        var response = history.Adapt<List<OrderHistoryItemResponse>>();

        return Ok(response);
    }
}
