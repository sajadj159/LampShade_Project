using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.A.ProductPicture
{
    public interface IProductPictureApplication
    {
        Task<OperationResult> CreateAsync(CreateProductPicture command, CancellationToken cancellationToken = default);
        Task<OperationResult> EditAsync(EditProductPicture command, CancellationToken cancellationToken = default);
        Task<OperationResult> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<OperationResult> RestoreAsync(long id, CancellationToken cancellationToken = default);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        EditProductPicture GetDetails(long id);
        
    }
}
