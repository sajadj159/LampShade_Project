using MediatR;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;

using ShopManagement.Application.Contracts.Commands.Slides.RestoreSlide;

namespace ShopManagement.Application.Features.Slides.Commands.RestoreSlide;



public class RestoreSlideCommandHandler : IRequestHandler<RestoreSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;
    public RestoreSlideCommandHandler(ISlideApplication application) => _application = application;
    public Task<OperationResult> Handle(RestoreSlideCommand r, CancellationToken c) => Task.FromResult(_application.Restore(r.Id));
}
