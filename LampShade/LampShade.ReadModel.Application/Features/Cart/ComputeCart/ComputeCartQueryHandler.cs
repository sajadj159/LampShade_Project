using MediatR;
using ShopManagement.Application.Contract.Order;
using LampShade.ReadModel.Contracts.Cart;

using LampShade.ReadModel.Contracts.Queries.Cart.ComputeCart;

namespace LampShade.ReadModel.Application.Features.Cart.ComputeCart;



public class ComputeCartQueryHandler : IRequestHandler<ComputeCartQuery, ShopManagement.Application.Contract.Order.Cart>
{
    private readonly ICartCalculatorService _cartCalculator;
    public ComputeCartQueryHandler(ICartCalculatorService cartCalculator) => _cartCalculator = cartCalculator;
    public Task<ShopManagement.Application.Contract.Order.Cart> Handle(ComputeCartQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_cartCalculator.ComputeCart(request.CartItems));
    }
}
