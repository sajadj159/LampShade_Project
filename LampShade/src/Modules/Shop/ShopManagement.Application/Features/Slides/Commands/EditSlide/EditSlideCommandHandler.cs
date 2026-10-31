using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Slides.EditSlide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Features.Slides.Commands.EditSlide;

public class EditSlideCommandHandler(ISlideRepository slides, IFileUploader uploader) : IRequestHandler<EditSlideCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditSlideCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var slide = await slides.GetAsync(request.Id, cancellationToken);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await slides.ExistAsync(x => x.PictureTitle == request.PictureTitle && x.Id != request.Id, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        slide.Edit(uploader.Upload(request.PictureUrl, "Slides"), request.PictureAlt, request.PictureTitle, request.Heading,
            request.Title, request.Text, request.BtnText, request.Link);
        return operation.Succeeded();
    }
}
