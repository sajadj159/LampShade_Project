using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;

namespace LampShade.Api.Features.ProductPictures.Queries.SearchProductPictures;

public class SearchProductPicturesQuery : IRequest<List<ProductPictureViewModel>>
{
    public long? ProductId { get; set; }
}

public class SearchProductPicturesQueryHandler : IRequestHandler<SearchProductPicturesQuery, List<ProductPictureViewModel>>
{
    private readonly IProductPictureApplication _application;

    public SearchProductPicturesQueryHandler(IProductPictureApplication application)
    {
        _application = application;
    }

    public async Task<List<ProductPictureViewModel>> Handle(SearchProductPicturesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ProductPictureSearchModel { ProductId = request.ProductId ?? 0 };
        return await Task.FromResult(_application.Search(searchModel));
    }
}

