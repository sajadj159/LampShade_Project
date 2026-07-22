using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Application.Features.Slides.Queries.GetSlideById;

public class GetSlideByIdQuery : IRequest<EditSlide>
{
    public long Id { get; set; }
}

public class GetSlideByIdQueryHandler : IRequestHandler<GetSlideByIdQuery, EditSlide>
{
    private readonly ISlideApplication _application;
    public GetSlideByIdQueryHandler(ISlideApplication application) => _application = application;
    public Task<EditSlide> Handle(GetSlideByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
