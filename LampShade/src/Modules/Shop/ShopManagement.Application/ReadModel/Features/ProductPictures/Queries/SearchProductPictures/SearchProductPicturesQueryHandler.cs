using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;

using ShopManagement.Application.ReadModel.Queries.ProductPictures.SearchProductPictures;

namespace ShopManagement.Application.ReadModel.Features.ProductPictures.Queries.SearchProductPictures;



public class SearchProductPicturesQueryHandler : IRequestHandler<SearchProductPicturesQuery, List<ProductPictureViewModel>>
{
    private readonly IProductPictureReadRepository _application;

    public SearchProductPicturesQueryHandler(IProductPictureReadRepository application)
    {
        _application = application;
    }

    public Task<List<ProductPictureViewModel>> Handle(SearchProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ProductPictureSearchModel { ProductId = request.ProductId ?? 0 };
        return Task.FromResult(_application.Search(searchModel));
    }
}
