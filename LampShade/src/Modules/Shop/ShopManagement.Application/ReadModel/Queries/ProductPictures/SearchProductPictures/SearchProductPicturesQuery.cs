#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.ProductPictures.SearchProductPictures;

public class SearchProductPicturesQuery : IRequest<List<ProductPictureViewModel>>
{
    public long? ProductId { get; set; }
}
