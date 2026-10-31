using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace LampShade.ReadModel.Contracts.Queries.Slides.GetSlides;

public class GetSlidesQuery : IRequest<List<SlideViewModel>> { }
