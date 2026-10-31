using MediatR;
using LampShade.BuildingBlocks.Application;
using ShopManagement.Application.Contract.A.Slide;

namespace ShopManagement.Application.Contracts.Commands.Slides.RestoreSlide;

public class RestoreSlideCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
