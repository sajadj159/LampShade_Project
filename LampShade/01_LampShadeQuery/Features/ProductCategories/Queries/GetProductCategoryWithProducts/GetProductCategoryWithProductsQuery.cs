using MediatR;
using _01_LampShadeQuery.Contract.ProductCategory;

namespace _01_LampShadeQuery.Features.ProductCategories.Queries.GetProductCategoryWithProducts;

public class GetProductCategoryWithProductsQuery : IRequest<ProductCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetProductCategoryWithProductsQueryHandler : IRequestHandler<GetProductCategoryWithProductsQuery, ProductCategoryQueryModel>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoryWithProductsQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<ProductCategoryQueryModel> Handle(GetProductCategoryWithProductsQuery r, CancellationToken c) => Task.FromResult(_query.GetProductCategoryWithProducts(r.Slug));
}
