#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ProductPictures.SearchProductPictures;

public class SearchProductPicturesQuery : IRequest<List<ProductPictureViewModel>>
{
    public long? ProductId { get; set; }
}
