using Mapster;
using Microsoft.AspNetCore.Mvc;
using Trading.Api.ViewModels.Responses;
using Trading.Infrastructure.DataAccess.Repositories.Interfaces;

namespace Trading.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetBySymbol(
        [FromRoute] string symbol,
        CancellationToken cancellationToken)
    {
        var history = await _orderRepository.GetBySymbolAsync(symbol, cancellationToken);

        var response = history.Adapt<List<OrderHistoryItemResponse>>();

        return Ok(response);
    }
}

