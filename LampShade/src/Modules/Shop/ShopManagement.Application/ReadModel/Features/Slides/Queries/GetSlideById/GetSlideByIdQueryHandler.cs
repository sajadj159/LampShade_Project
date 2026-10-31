using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.A.Slide;

using ShopManagement.Application.ReadModel.Queries.Slides.GetSlideById;

namespace ShopManagement.Application.ReadModel.Features.Slides.Queries.GetSlideById;



public class GetSlideByIdQueryHandler : IRequestHandler<GetSlideByIdQuery, EditSlide>
{
    private readonly ISlideReadRepository _application;
    public GetSlideByIdQueryHandler(ISlideReadRepository application) => _application = application;
    public Task<EditSlide> Handle(GetSlideByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
