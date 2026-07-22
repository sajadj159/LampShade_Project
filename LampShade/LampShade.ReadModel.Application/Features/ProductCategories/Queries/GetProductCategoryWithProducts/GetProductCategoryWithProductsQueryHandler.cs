using MediatR;
using LampShade.ReadModel.Contracts.ProductCategory;

using LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoryWithProducts;

namespace LampShade.ReadModel.Application.Features.ProductCategories.Queries.GetProductCategoryWithProducts;



public class GetProductCategoryWithProductsQueryHandler : IRequestHandler<GetProductCategoryWithProductsQuery, ProductCategoryQueryModel>
{
    private readonly IProductCategoryQuery _query;
    public GetProductCategoryWithProductsQueryHandler(IProductCategoryQuery query) => _query = query;
    public Task<ProductCategoryQueryModel> Handle(GetProductCategoryWithProductsQuery r, CancellationToken c) => Task.FromResult(_query.GetProductCategoryWithProducts(r.Slug));
}
