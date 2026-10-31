using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.ReadModel.Product;

public interface IProductQuery
{
    Task<ProductQueryModel> GetProductDetailsAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<ProductQueryModel>> GetLatestArrivalsAsync(CancellationToken cancellationToken = default);
    Task<List<ProductQueryModel>> SearchAsync(string value, CancellationToken cancellationToken = default);
    Task<List<CartItem>> CheckInventoryStatusAsync(List<CartItem> cartItems, CancellationToken cancellationToken = default);
}