using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Application.ReadModel.Queries.Slides.GetSlideById;

public class GetSlideByIdQuery : IRequest<EditSlide>
{
    public long Id { get; set; }
}
