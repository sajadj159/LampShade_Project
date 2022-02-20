using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategoryQueries();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}