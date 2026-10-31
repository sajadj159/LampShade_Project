using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.A.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);
        Product GetProductWithCategories(long id);
        Task<Product> GetProductWithCategoriesAsync(long id, CancellationToken cancellationToken = default);
        List<ProductViewModel> GetProducts();
    }
}
