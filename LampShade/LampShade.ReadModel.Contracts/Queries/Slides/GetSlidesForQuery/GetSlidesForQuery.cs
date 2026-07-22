using MediatR;
using LampShade.ReadModel.Contracts.Slide;

namespace LampShade.ReadModel.Contracts.Queries.Slides.GetSlidesForQuery;

public class GetSlidesForQuery : IRequest<List<SlideQueryModel>> { }
