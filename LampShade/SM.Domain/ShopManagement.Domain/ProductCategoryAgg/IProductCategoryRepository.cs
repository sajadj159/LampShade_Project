using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        List<ProductCategory> GetAll();
        ProductCategory Get(long id);

    }
}