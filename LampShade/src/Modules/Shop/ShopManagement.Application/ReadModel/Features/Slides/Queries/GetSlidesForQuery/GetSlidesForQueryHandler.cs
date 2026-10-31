using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.Slide;

using QueryRequest = LampShade.ReadModel.Contracts.Queries.Slides.GetSlidesForQuery.GetSlidesForQuery;

namespace LampShade.ReadModel.Application.Features.Slides.Queries.GetSlidesForQuery;



public class GetSlidesForQueryHandler : IRequestHandler<QueryRequest, List<SlideQueryModel>>
{
    private readonly ISlideQuery _query;
    public GetSlidesForQueryHandler(ISlideQuery query) => _query = query;
    public Task<List<SlideQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetSlidesAsync(c);
}
