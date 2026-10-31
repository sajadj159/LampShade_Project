using LampShade.ReadModel.Contracts.Product;
using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Contracts.Queries.Products.CheckInventoryStatus;

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

