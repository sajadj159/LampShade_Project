using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        Task<OperationResult> CreateAsync(CreateProductCategory command, CancellationToken cancellationToken = default);
        Task<OperationResult> EditAsync(EditProductCategory command, CancellationToken cancellationToken = default);
        Task<OperationResult> DeleteAsync(long id, CancellationToken cancellationToken = default);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();

    }
}
