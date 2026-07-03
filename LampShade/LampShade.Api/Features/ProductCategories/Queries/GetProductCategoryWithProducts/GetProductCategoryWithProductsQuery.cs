using MediatR;
using _01_LampShadeQuery.Contract.ProductCategory;

namespace LampShade.Api.Features.ProductCategories.Queries.GetProductCategoryWithProducts;

public class GetProductCategoryWithProductsQuery : IRequest<ProductCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetProductCategoryWithProductsQueryHandler : IRequestHandler<GetProductCategoryWithProductsQuery, ProductCategoryQueryModel>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoryWithProductsQueryHandler(IProductCategoryQuery query) => _query = query;
    public async Task<ProductCategoryQueryModel> Handle(GetProductCategoryWithProductsQuery r, CancellationToken c) => await Task.FromResult(_query.GetProductCategoryWithProducts(r.Slug));
}
