using Mapster;
using Microsoft.AspNetCore.Mvc;
using Trading.Api.ViewModels.Requests;
using Trading.Api.ViewModels.Responses;
using Trading.TradingEngine.TradingRules.Services.Interfaces;

namespace Trading.Api.Controllers;

[ApiController]
[Route("api/trading-rules")]
public class TradingRulesController : ControllerBase
{
    private readonly ITradingRulesService _tradingRulesService;

    public TradingRulesController(ITradingRulesService tradingRulesService)
    {
        _tradingRulesService = tradingRulesService;
    }

    [HttpGet]
    public IActionResult GetCurrent()
    {
        var current = _tradingRulesService.GetCurrent();
        var response = current.Adapt<TradingRulesResponse>();

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] TradingRulesUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _tradingRulesService.UpdateAsync(
            request.MaxNotional,
            request.MaxQuantity,
            request.PriceDeviationPercent,
            request.DuplicateIdPreventionEnabled,
            request.SymbolWhitelistEnabled,
            request.SymbolWhitelist,
            cancellationToken);

        var response = updated.Adapt<TradingRulesResponse>();

        return Ok(response);
    }
}