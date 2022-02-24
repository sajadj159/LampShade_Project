using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.A.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        ProductPicture GetWithProductsAndCategories(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}