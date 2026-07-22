using _0_Framework.Repository;
using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;
using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;
using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;
using ShopManagement.Application.Contracts.Commands.Orders.PaymentSucceeded;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Application.Contracts.Commands.Orders.UploadPaymentProof;
using LampShade.ReadModel.Contracts.Queries.Orders.GetOrderAmount;
using LampShade.ReadModel.Contracts.Queries.Orders.GetOrderItems;
using LampShade.ReadModel.Contracts.Queries.Orders.SearchOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize]
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

    [Authorize(Roles = Roles.Administrator)]
    [HttpPost("{id}/approve-cash-on-delivery")]
    public async Task<IActionResult> ApproveCashOnDelivery(long id)
    {
        var trackingNumber = await _mediator.Send(new ApproveCashOnDeliveryCommand { OrderId = id });
        return string.IsNullOrWhiteSpace(trackingNumber)
            ? BadRequest(new { Message = "The order cannot be approved." })
            : Ok(new { IssueTrackingNumber = trackingNumber });
    }
    [HttpPost("{orderId}/payment-succeeded")]
    public async Task<IActionResult> PaymentSucceeded(long orderId, [FromQuery] long refId)
        => Ok(new { IssueTrackingNumber = await _mediator.Send(new PaymentSucceededCommand { OrderId = orderId, RefId = refId }) });

    [HttpPost("{id}/payment-proof")]
    public async Task<IActionResult> UploadPaymentProof(long id, [FromForm] UploadPaymentProofCommand command)
    {
        command.OrderId = id;
        return Ok(await _mediator.Send(command));
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPost("{id}/approve-payment-proof")]
    public async Task<IActionResult> ApprovePaymentProof(long id)
    {
        var trackingNumber = await _mediator.Send(new ApprovePaymentProofCommand { OrderId = id });
        return string.IsNullOrWhiteSpace(trackingNumber)
            ? BadRequest(new { Message = "The order cannot be approved." })
            : Ok(new { IssueTrackingNumber = trackingNumber });
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(long id) { await _mediator.Send(new CancelOrderCommand { Id = id }); return Ok(); }

    [Authorize(Roles = Roles.Administrator)]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchOrdersQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsBy(long id) => Ok(await _mediator.Send(new GetOrderItemsQuery { OrderId = id }));
}





