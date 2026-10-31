using LampShade.BuildingBlocks.Domain;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        Task<ProductPicture> GetWithProductsAndCategoriesAsync(long id, CancellationToken cancellationToken = default);
    }
}
