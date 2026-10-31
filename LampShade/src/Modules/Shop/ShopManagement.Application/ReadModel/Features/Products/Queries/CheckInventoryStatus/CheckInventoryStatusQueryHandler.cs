using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.ReadModel.Product;
using MediatR;
using ShopManagement.Application.Contract.Order;

using ShopManagement.Application.ReadModel.Queries.Products.CheckInventoryStatus;

namespace ShopManagement.Application.ReadModel.Features.Products.Queries.CheckInventoryStatus;





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
        return _productQuery.CheckInventoryStatusAsync(cartItems, cancellationToken);
    }
}
