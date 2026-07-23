using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        string GetSlugBy(long id);
        Task<string> GetSlugByAsync(long id, CancellationToken cancellationToken = default);
        bool HasProducts(long id);
        Task<bool> HasProductsAsync(long id, CancellationToken cancellationToken = default);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
