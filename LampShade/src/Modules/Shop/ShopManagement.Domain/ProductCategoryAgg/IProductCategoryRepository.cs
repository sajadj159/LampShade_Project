using LampShade.BuildingBlocks.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        Task<string> GetSlugByAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> HasProductsAsync(long id, CancellationToken cancellationToken = default);
    }
}
