using LampShade.BuildingBlocks.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrdersByAccount;
using ShopManagement.Application.ReadModel.Queries.Orders.GetPaidOrders;

namespace ShopManagement.Presentation.Controllers.Read;

[Authorize]
[ApiController]
[Route("api/read/[controller]")]
public class OrderQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderQueryController(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = Roles.Administrator)]
    [HttpGet("paid")]
    public async Task<IActionResult> GetPayedOrders() => Ok(await _mediator.Send(new GetPaidOrdersQuery()));

    /// <summary>A customer's order history. Non-admin callers always get their own orders, whatever id they pass.</summary>
    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetOrders(long accountId) => Ok(await _mediator.Send(new GetOrdersByAccountQuery { AccountId = accountId }));
}
