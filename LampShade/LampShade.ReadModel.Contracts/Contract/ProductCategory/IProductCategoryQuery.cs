using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProducts(string slug);
        List<ProductCategoryQueryModel> GetProductCategoryQueries();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}