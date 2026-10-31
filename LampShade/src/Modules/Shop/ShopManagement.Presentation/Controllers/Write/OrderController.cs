using LampShade.BuildingBlocks.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;
using ShopManagement.Application.Contracts.Commands.Orders.ApprovePaymentProof;
using ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Application.Contracts.Commands.Orders.UploadPaymentProof;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrderAmount;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrderItems;
using ShopManagement.Application.ReadModel.Queries.Orders.SearchOrders;

namespace ShopManagement.Presentation.Controllers.Write;

[Authorize]
[ApiController]
[Route("api/write/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator) => _mediator = mediator;

    /// <summary>Places an order from product ids and counts; the server prices it again and checks stock.</summary>
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderCommand command)
    {
        var order = await _mediator.Send(command);
        return Ok(new { order.OrderId });
    }

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
    public async Task<IActionResult> Cancel(long id) => Ok(await _mediator.Send(new CancelOrderCommand { Id = id }));

    [Authorize(Roles = Roles.Administrator)]
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchOrdersQuery query) => Ok(await _mediator.Send(query));

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsBy(long id) => Ok(await _mediator.Send(new GetOrderItemsQuery { OrderId = id }));
}
