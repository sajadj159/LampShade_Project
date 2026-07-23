using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.A.Slide
{
    public interface ISlideApplication
    {
        Task<OperationResult> CreateAsync(CreateSlide command, CancellationToken cancellationToken = default);
        Task<OperationResult> EditAsync(EditSlide command, CancellationToken cancellationToken = default);
        Task<OperationResult> RemoveAsync(long id, CancellationToken cancellationToken = default);
        Task<OperationResult> RestoreAsync(long id, CancellationToken cancellationToken = default);
        List<SlideViewModel> GetList();
        EditSlide GetDetails(long id);
    }
}
