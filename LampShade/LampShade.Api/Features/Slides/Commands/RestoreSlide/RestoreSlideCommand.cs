using MediatR;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;

namespace LampShade.Api.Features.Slides.Commands.RestoreSlide;

public class RestoreSlideCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RestoreSlideCommandHandler : IRequestHandler<RestoreSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;
    public RestoreSlideCommandHandler(ISlideApplication application) => _application = application;
    public async Task<OperationResult> Handle(RestoreSlideCommand r, CancellationToken c) => await Task.FromResult(_application.Restore(r.Id));
}
