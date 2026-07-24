namespace LampShade.ReadModel.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    Task<ProductCategoryQueryModel> GetProductCategoryWithProductsAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<ProductCategoryQueryModel>> GetProductCategoryQueriesAsync(CancellationToken cancellationToken = default);
    Task<List<ProductCategoryQueryModel>> GetProductCategoriesWithProductsAsync(CancellationToken cancellationToken = default);
}