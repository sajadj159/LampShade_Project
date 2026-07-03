using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace LampShade.Api.Features.Slides.Queries.GetSlides;

public class GetSlidesQuery : IRequest<List<SlideViewModel>> { }

public class GetSlidesQueryHandler : IRequestHandler<GetSlidesQuery, List<SlideViewModel>>
{
    private readonly ISlideApplication _application;
    public GetSlidesQueryHandler(ISlideApplication application) => _application = application;
    public async Task<List<SlideViewModel>> Handle(GetSlidesQuery r, CancellationToken c) => await Task.FromResult(_application.GetList());
}
