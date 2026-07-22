using MediatR;
using ShopManagement.Application.Contract.A.Slide;

using LampShade.ReadModel.Contracts.Queries.Slides.GetSlides;

namespace LampShade.ReadModel.Application.Features.Slides.Queries.GetSlides;



public class GetSlidesQueryHandler : IRequestHandler<GetSlidesQuery, List<SlideViewModel>>
{
    private readonly ISlideApplication _application;
    public GetSlidesQueryHandler(ISlideApplication application) => _application = application;
    public Task<List<SlideViewModel>> Handle(GetSlidesQuery r, CancellationToken c) => Task.FromResult(_application.GetList());
}
