using MediatR;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Application.Contracts.Commands.Slides.RemoveSlide;

public class RemoveSlideCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
