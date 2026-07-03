using MediatR;
using ShopManagement.Application.Contract.Order;
using _01_LampShadeQuery.Contract.Cart;

namespace LampShade.Api.Features.Cart.ComputeCart;

public class ComputeCartQuery : IRequest<ShopManagement.Application.Contract.Order.Cart>
{
    public List<CartItem> CartItems { get; set; } = new();
}

public class ComputeCartQueryHandler : IRequestHandler<ComputeCartQuery, ShopManagement.Application.Contract.Order.Cart>
{
    private readonly ICartCalculatorService _cartCalculator;
    public ComputeCartQueryHandler(ICartCalculatorService cartCalculator) => _cartCalculator = cartCalculator;
    public async Task<ShopManagement.Application.Contract.Order.Cart> Handle(ComputeCartQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_cartCalculator.ComputeCart(request.CartItems));
    }
}
