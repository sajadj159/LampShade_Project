using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ProductPictures.GetProductPictureById;

public class GetProductPictureByIdQuery : IRequest<EditProductPicture>
{
    public long Id { get; set; }
}
