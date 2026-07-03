using MediatR;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;

namespace LampShade.Api.Features.Slides.Commands.RemoveSlide;

public class RemoveSlideCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RemoveSlideCommandHandler : IRequestHandler<RemoveSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;
    public RemoveSlideCommandHandler(ISlideApplication application) => _application = application;
    public async Task<OperationResult> Handle(RemoveSlideCommand r, CancellationToken c) => await Task.FromResult(_application.Remove(r.Id));
}
