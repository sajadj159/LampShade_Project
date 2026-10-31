using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.A.Slide;

using LampShade.ReadModel.Contracts.Queries.Slides.GetSlideById;

namespace LampShade.ReadModel.Application.Features.Slides.Queries.GetSlideById;



public class GetSlideByIdQueryHandler : IRequestHandler<GetSlideByIdQuery, EditSlide>
{
    private readonly ISlideApplication _application;
    public GetSlideByIdQueryHandler(ISlideApplication application) => _application = application;
    public Task<EditSlide> Handle(GetSlideByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
