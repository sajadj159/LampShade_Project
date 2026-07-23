using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.A.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        ProductPicture GetWithProductsAndCategories(long id);
        Task<ProductPicture> GetWithProductsAndCategoriesAsync(long id, CancellationToken cancellationToken = default);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
