using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductPictures.RemoveProductPicture;

public class RemoveProductPictureCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
