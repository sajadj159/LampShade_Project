using LampShade.Api.Features.Orders.Queries.GetOrdersByAccount;
using LampShade.Api.Features.Orders.Queries.GetPaidOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Read;

[ApiController]
[Route("api/read/[controller]")]
public class OrderQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderQueryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("paid")]
    public async Task<IActionResult> GetPayedOrders() => Ok(await _mediator.Send(new GetPaidOrdersQuery()));

    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetOrders(long accountId) => Ok(await _mediator.Send(new GetOrdersByAccountQuery { AccountId = accountId }));
}
