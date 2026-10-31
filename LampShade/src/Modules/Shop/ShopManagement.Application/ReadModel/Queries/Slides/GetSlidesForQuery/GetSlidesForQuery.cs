using MediatR;
using ShopManagement.Application.ReadModel.Slide;

namespace ShopManagement.Application.ReadModel.Queries.Slides.GetSlidesForQuery;

public class GetSlidesForQuery : IRequest<List<SlideQueryModel>> { }
