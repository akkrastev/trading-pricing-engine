using Mapster;
using Microsoft.AspNetCore.Mvc;
using Trading.Api.ViewModels.Responses;
using Trading.TradingEngine.MarketDataProcessing.Services.Interfaces;

namespace Trading.Api.Controllers;

[ApiController]
[Route("api/prices")]
public class PricesController : ControllerBase
{
    private readonly IPriceStateStore _priceStateStore;

    public PricesController(IPriceStateStore priceStateStore)
    {
        _priceStateStore = priceStateStore;
    }

    [HttpGet("{symbol}")]
    public IActionResult GetLatest([FromRoute] string symbol)
    {
        var state = _priceStateStore.GetState(symbol);

        if (state is null)
        {
            return NotFound();
        }

        var response = state.Current.Adapt<LatestPriceResponse>();

        return Ok(response);
    }
}