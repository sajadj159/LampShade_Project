using MediatR;
using _01_LampShadeQuery.Contract.ProductCategory;

namespace LampShade.Api.Features.ProductCategories.Queries.GetProductCategoriesWithProductsForQuery;

public class GetProductCategoriesWithProductsForQuery : IRequest<List<ProductCategoryQueryModel>> { }

public class GetProductCategoriesWithProductsForQueryHandler : IRequestHandler<GetProductCategoriesWithProductsForQuery, List<ProductCategoryQueryModel>>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoriesWithProductsForQueryHandler(IProductCategoryQuery query) => _query = query;
    public async Task<List<ProductCategoryQueryModel>> Handle(GetProductCategoriesWithProductsForQuery r, CancellationToken c) => await Task.FromResult(_query.GetProductCategoriesWithProducts());
}
