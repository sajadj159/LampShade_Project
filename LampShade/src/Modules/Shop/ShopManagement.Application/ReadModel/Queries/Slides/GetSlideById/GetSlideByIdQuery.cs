using MediatR;
using ShopManagement.Application.Contract.A.Slide;

namespace LampShade.ReadModel.Contracts.Queries.Slides.GetSlideById;

public class GetSlideByIdQuery : IRequest<EditSlide>
{
    public long Id { get; set; }
}
