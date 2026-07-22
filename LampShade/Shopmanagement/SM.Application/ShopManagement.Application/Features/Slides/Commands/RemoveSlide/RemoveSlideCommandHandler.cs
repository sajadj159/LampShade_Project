using MediatR;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;

using ShopManagement.Application.Contracts.Commands.Slides.RemoveSlide;

namespace ShopManagement.Application.Features.Slides.Commands.RemoveSlide;



public class RemoveSlideCommandHandler : IRequestHandler<RemoveSlideCommand, OperationResult>
{
    private readonly ISlideApplication _application;
    public RemoveSlideCommandHandler(ISlideApplication application) => _application = application;
    public Task<OperationResult> Handle(RemoveSlideCommand r, CancellationToken c) => Task.FromResult(_application.Remove(r.Id));
}
