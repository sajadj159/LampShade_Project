using MediatR;
using _01_LampShadeQuery.Contract.Slide;

namespace _01_LampShadeQuery.Features.Slides.Queries.GetSlidesForQuery;

public class GetSlidesForQuery : IRequest<List<SlideQueryModel>> { }

public class GetSlidesForQueryHandler : IRequestHandler<GetSlidesForQuery, List<SlideQueryModel>>
{
    private readonly ISlideQuery _query;
    public GetSlidesForQueryHandler(ISlideQuery query) => _query = query;
    public Task<List<SlideQueryModel>> Handle(GetSlidesForQuery r, CancellationToken c) => Task.FromResult(_query.GetSlides());
}
