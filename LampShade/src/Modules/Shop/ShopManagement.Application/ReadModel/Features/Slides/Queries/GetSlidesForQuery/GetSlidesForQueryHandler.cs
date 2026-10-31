using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Slide;

using QueryRequest = ShopManagement.Application.ReadModel.Queries.Slides.GetSlidesForQuery.GetSlidesForQuery;

namespace ShopManagement.Application.ReadModel.Features.Slides.Queries.GetSlidesForQuery;



public class GetSlidesForQueryHandler : IRequestHandler<QueryRequest, List<SlideQueryModel>>
{
    private readonly ISlideQuery _query;
    public GetSlidesForQueryHandler(ISlideQuery query) => _query = query;
    public Task<List<SlideQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetSlidesAsync(c);
}
