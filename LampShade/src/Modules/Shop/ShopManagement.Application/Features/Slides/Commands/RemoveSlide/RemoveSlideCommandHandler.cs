using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Slides.RemoveSlide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Features.Slides.Commands.RemoveSlide;

public class RemoveSlideCommandHandler(ISlideRepository slides) : IRequestHandler<RemoveSlideCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RemoveSlideCommand request, CancellationToken cancellationToken)
    {
        var slide = await slides.GetAsync(request.Id, cancellationToken);
        if (slide is null)
            return new OperationResult().Failed(ApplicationMessages.RecordNotFound);
        slide.Remove();
        return new OperationResult().Succeeded();
    }
}
