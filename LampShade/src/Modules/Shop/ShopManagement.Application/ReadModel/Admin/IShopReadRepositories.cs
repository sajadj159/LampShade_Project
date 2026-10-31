using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Application.Contract.A.Slide;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.ReadModel.Admin;

// Admin-panel projections of Shop's tables. They are reads, so they live beside the queries
// rather than on the domain repositories, which only load and save aggregates.

public interface IProductReadRepository
{
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    EditProduct GetDetails(long id);
    List<ProductViewModel> GetProducts();
}

public interface IProductCategoryReadRepository
{
    List<ProductCategoryViewModel> GetProductCategories();
    EditProductCategory GetDetails(long id);
    List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
}

public interface IProductPictureReadRepository
{
    EditProductPicture GetDetails(long id);
    List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
}

public interface ISlideReadRepository
{
    EditSlide GetDetails(long id);
    List<SlideViewModel> GetList();
}

public interface IOrderReadRepository
{
    double GetAmountBy(long id);
    Task<List<OrderViewModel>> SearchAsync(OrderSearchModel searchModel, CancellationToken cancellationToken = default);
    List<OrderItemViewModel> GetItemsBy(long orderId);
}
