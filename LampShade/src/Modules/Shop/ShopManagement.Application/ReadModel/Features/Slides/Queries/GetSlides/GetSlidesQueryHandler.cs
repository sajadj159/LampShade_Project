using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.A.Slide;

using ShopManagement.Application.ReadModel.Queries.Slides.GetSlides;

namespace ShopManagement.Application.ReadModel.Features.Slides.Queries.GetSlides;



public class GetSlidesQueryHandler : IRequestHandler<GetSlidesQuery, List<SlideViewModel>>
{
    private readonly ISlideReadRepository _application;
    public GetSlidesQueryHandler(ISlideReadRepository application) => _application = application;
    public Task<List<SlideViewModel>> Handle(GetSlidesQuery r, CancellationToken c) => Task.FromResult(_application.GetList());
}
