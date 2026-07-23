using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductPictures.RestoreProductPicture;

public class RestoreProductPictureCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
