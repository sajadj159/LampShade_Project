using LampShade.Api.Features.Orders.Commands.CancelOrder;
using LampShade.Api.Features.Orders.Commands.PaymentSucceeded;
using LampShade.Api.Features.Orders.Commands.PlaceOrder;
using LampShade.Api.Features.Orders.Queries.GetOrderAmount;
using LampShade.Api.Features.Orders.Queries.GetOrderItems;
using LampShade.Api.Features.Orders.Queries.SearchOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[ApiController]
[Route("api/write/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderCommand command) => Ok(new { OrderId = await _mediator.Send(command) });

    [HttpGet("{id}/amount")]
    public async Task<IActionResult> GetAmountBy(long id) => Ok(new { Amount = await _mediator.Send(new GetOrderAmountQuery { Id = id }) });

    [HttpPost("{orderId}/payment-succeeded")]
    public async Task<IActionResult> PaymentSucceeded(long orderId, [FromQuery] long refId)
        => Ok(new { IssueTrackingNumber = await _mediator.Send(new PaymentSucceededCommand { OrderId = orderId, RefId = refId }) });

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(long id) { await _mediator.Send(new CancelOrderCommand { Id = id }); return Ok(); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchOrdersQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsBy(long id) => Ok(await _mediator.Send(new GetOrderItemsQuery { OrderId = id }));
}
