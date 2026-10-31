using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.ProductPictures.SearchProductPictures;

namespace LampShade.ReadModel.Application.Features.ProductPictures.Queries.SearchProductPictures;



public class SearchProductPicturesQueryHandler : IRequestHandler<SearchProductPicturesQuery, List<ProductPictureViewModel>>
{
    private readonly IProductPictureApplication _application;

    public SearchProductPicturesQueryHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public Task<List<ProductPictureViewModel>> Handle(SearchProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ProductPictureSearchModel { ProductId = request.ProductId ?? 0 };
        return Task.FromResult(_application.Search(searchModel));
    }
}
