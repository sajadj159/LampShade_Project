using MediatR;
using ShopManagement.Application.Orders;
using ShopManagement.Application.ReadModel.Queries.Cart.ComputeCart;

namespace ShopManagement.Application.ReadModel.Features.Cart.ComputeCart;

public class ComputeCartQueryHandler(CartCalculator calculator) : IRequestHandler<ComputeCartQuery, Contract.Order.Cart>
{
    public Task<Contract.Order.Cart> Handle(ComputeCartQuery request, CancellationToken cancellationToken) =>
        calculator.ComputeAsync(request.CartItems, cancellationToken);
}
