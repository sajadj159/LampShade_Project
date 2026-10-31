using System.Threading;
using System.Threading.Tasks;
using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;

using ShopManagement.Application.ReadModel.Queries.ProductPictures.GetProductPictureById;

namespace ShopManagement.Application.ReadModel.Features.ProductPictures.Queries.GetProductPictureById;



public class GetProductPictureByIdQueryHandler : IRequestHandler<GetProductPictureByIdQuery, EditProductPicture>
{
    private readonly IProductPictureReadRepository _application;

    public GetProductPictureByIdQueryHandler(IProductPictureReadRepository application)
    {
        _application = application;
    }

    public Task<EditProductPicture> Handle(GetProductPictureByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
