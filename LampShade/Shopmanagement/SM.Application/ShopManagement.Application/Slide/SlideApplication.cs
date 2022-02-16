using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Slide
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operationResult = new OperationResult();
            if (_slideRepository.Exist(x => x.PictureTitle == command.PictureTitle))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            var slide = new Domain.SlideAgg.Slide(command.PictureUrl, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepository.Create(slide);
            _slideRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operationResult = new OperationResult();
            var editSlide = _slideRepository.Get(command.Id);
            if (editSlide == null)
            {
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_slideRepository.Exist(x => x.PictureTitle == command.PictureTitle && x.Id != command.Id))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            editSlide.Edit(command.PictureUrl, command.PictureAlt, command.PictureTitle, command.Heading, command.Title,
                command.Text, command.BtnText,command.Link);
            _slideRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            slide.Remove();
            _slideRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            slide.Restore();
            _slideRepository.Save();
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