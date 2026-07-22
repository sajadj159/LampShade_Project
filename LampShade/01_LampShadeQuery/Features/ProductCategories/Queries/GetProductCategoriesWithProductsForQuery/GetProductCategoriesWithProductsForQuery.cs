using MediatR;
using _01_LampShadeQuery.Contract.ProductCategory;

namespace _01_LampShadeQuery.Features.ProductCategories.Queries.GetProductCategoriesWithProductsForQuery;

public class GetProductCategoriesWithProductsForQuery : IRequest<List<ProductCategoryQueryModel>> { }

public class GetProductCategoriesWithProductsForQueryHandler : IRequestHandler<GetProductCategoriesWithProductsForQuery, List<ProductCategoryQueryModel>>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoriesWithProductsForQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<List<ProductCategoryQueryModel>> Handle(GetProductCategoriesWithProductsForQuery r, CancellationToken c) => Task.FromResult(_query.GetProductCategoriesWithProducts());
}
