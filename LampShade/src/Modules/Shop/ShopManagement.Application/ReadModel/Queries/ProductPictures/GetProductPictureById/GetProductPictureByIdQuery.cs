using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.ProductPictures.GetProductPictureById;

public class GetProductPictureByIdQuery : IRequest<EditProductPicture>
{
    public long Id { get; set; }
}
