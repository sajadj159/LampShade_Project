using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Application.ReadModel.Queries.Slides.GetSlides;

public class GetSlidesQuery : IRequest<List<SlideViewModel>> { }
