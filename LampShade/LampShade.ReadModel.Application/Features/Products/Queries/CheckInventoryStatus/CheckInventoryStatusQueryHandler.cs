using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Product;
using MediatR;
using ShopManagement.Application.Contract.Order;

using LampShade.ReadModel.Contracts.Queries.Products.CheckInventoryStatus;

namespace LampShade.ReadModel.Application.Features.Products.Queries.CheckInventoryStatus;





public class CheckInventoryStatusQueryHandler : IRequestHandler<CheckInventoryStatusQuery, List<CartItem>>
{
    private readonly IProductQuery _productQuery;

    public CheckInventoryStatusQueryHandler(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public Task<List<CartItem>> Handle(CheckInventoryStatusQuery request, CancellationToken cancellationToken)
    {
        var cartItems = request.CartItems.Select(i => new CartItem
        {
            Id = i.Id,
            Name = i.Name,
            UnitPrice = i.UnitPrice,
            PictureUrl = i.PictureUrl,
            Count = i.Count,
            DiscountRate = i.DiscountRate
        }).ToList();
        return Task.FromResult(_productQuery.CheckInventoryStatus(cartItems));
    }
}
