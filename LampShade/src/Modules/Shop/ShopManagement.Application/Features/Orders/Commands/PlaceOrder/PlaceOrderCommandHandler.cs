using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Features.Orders.Commands.PlaceOrder;

public class PlaceOrderCommandHandler(IOrderApplication application) : IRequestHandler<PlaceOrderCommand, PlaceOrderResult>
{
    public Task<PlaceOrderResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken) => application.PlaceOrderAsync(new ShopManagement.Application.Contract.Order.Cart { TotalAmount = request.TotalAmount, DiscountAmount = request.DiscountAmount, PayAmount = request.PayAmount, PaymentMethod = request.PaymentMethod, Items = request.Items }, cancellationToken);
}
