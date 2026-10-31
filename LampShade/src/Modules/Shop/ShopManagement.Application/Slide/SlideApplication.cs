using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Slide
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFIleUploader _uploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFIleUploader uploader)
        {
            _slideRepository = slideRepository;
            _uploader = uploader;
        }

        public async Task<OperationResult> CreateAsync(CreateSlide command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            if (await _slideRepository.ExistAsync(x => x.PictureTitle == command.PictureTitle, cancellationToken))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var picturePath = _uploader.Upload(command.PictureUrl,"Slides");
            var slide = new Domain.SlideAgg.Slide(picturePath, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepository.Add(slide);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> EditAsync(EditSlide command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var editSlide = await _slideRepository.GetAsync(command.Id, cancellationToken);
            if (editSlide == null)
            {
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (await _slideRepository.ExistAsync(x => x.PictureTitle == command.PictureTitle && x.Id != command.Id, cancellationToken))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var pictureUrl = _uploader.Upload(command.PictureUrl,"Slides");
            editSlide.Edit(pictureUrl, command.PictureAlt, command.PictureTitle, command.Heading, command.Title,
                command.Text, command.BtnText,command.Link);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var slide = await _slideRepository.GetAsync(id, cancellationToken);
            if (slide == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            slide.Remove();
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> RestoreAsync(long id, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var slide = await _slideRepository.GetAsync(id, cancellationToken);
            if (slide == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            slide.Restore();
            return operationResult.Succeeded();

        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }
    }
}
