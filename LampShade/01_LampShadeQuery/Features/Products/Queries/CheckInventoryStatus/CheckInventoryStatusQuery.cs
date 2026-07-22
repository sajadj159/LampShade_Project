using _01_LampShadeQuery.Contract.Product;
using MediatR;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Features.Products.Queries.CheckInventoryStatus;

public class CheckInventoryStatusQuery : IRequest<List<CartItem>>
{
    public List<CartItemRequest> CartItems { get; set; } = new();
}

public class CartItemRequest
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public int Count { get; set; }
    public int DiscountRate { get; set; }
}

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
