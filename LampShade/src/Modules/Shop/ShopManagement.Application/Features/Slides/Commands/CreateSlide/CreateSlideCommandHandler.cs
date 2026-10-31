using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Slides.CreateSlide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Features.Slides.Commands.CreateSlide;

public class CreateSlideCommandHandler(ISlideRepository slides, IFileUploader uploader) : IRequestHandler<CreateSlideCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await slides.ExistAsync(x => x.PictureTitle == request.PictureTitle, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        slides.Add(new Domain.SlideAgg.Slide(uploader.Upload(request.PictureUrl, "Slides"), request.PictureAlt, request.PictureTitle,
            request.Heading, request.Title, request.Text, request.BtnText, request.Link));
        return operation.Succeeded();
    }
}
