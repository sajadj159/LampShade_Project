using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Commands.PlaceOrder;

public class PlaceOrderCommand : IRequest<long>
{
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double PayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> Items { get; set; } = new();
}

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
