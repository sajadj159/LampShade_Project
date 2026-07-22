using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;

namespace ShopManagement.Application.Features.Orders.Commands.PlaceOrder;



public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, long>
{
    private readonly IOrderApplication _application;
    public PlaceOrderCommandHandler(IOrderApplication application) => _application = application;

    public Task<long> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = new ShopManagement.Application.Contract.Order.Cart
        {
            TotalAmount = request.TotalAmount,
            DiscountAmount = request.DiscountAmount,
            PayAmount = request.PayAmount,
            PaymentMethod = request.PaymentMethod,
            Items = request.Items
        };
        return Task.FromResult(_application.PlaceOrder(cart));
    }
}
