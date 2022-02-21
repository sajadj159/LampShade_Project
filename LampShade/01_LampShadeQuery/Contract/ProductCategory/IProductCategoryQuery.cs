using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProducts(string slug);
        List<ProductCategoryQueryModel> GetProductCategoryQueries();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}