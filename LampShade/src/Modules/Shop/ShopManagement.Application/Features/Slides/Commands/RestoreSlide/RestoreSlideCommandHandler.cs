using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contracts.Commands.Slides.RestoreSlide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Features.Slides.Commands.RestoreSlide;

public class RestoreSlideCommandHandler(ISlideRepository slides) : IRequestHandler<RestoreSlideCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RestoreSlideCommand request, CancellationToken cancellationToken)
    {
        var slide = await slides.GetAsync(request.Id, cancellationToken);
        if (slide is null)
            return new OperationResult().Failed(ApplicationMessages.RecordNotFound);
        slide.Restore();
        return new OperationResult().Succeeded();
    }
}
