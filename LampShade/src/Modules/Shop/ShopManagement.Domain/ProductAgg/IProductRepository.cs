using LampShade.BuildingBlocks.Domain;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        Task<Product> GetProductWithCategoriesAsync(long id, CancellationToken cancellationToken = default);
    }
}
