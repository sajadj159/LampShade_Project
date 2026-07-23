using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.A.Product
{
    public interface IProductApplication
    {
        Task<OperationResult> CreateAsync(CreateProduct command, CancellationToken cancellationToken = default);
        Task<OperationResult> EditAsync(EditProduct command, CancellationToken cancellationToken = default);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
        EditProduct GetDetails(long id);
    }
}
