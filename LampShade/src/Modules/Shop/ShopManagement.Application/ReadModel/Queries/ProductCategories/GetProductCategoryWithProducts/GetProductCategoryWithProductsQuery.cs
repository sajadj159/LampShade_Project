using MediatR;
using LampShade.ReadModel.Contracts.ProductCategory;

namespace LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoryWithProducts;

public class GetProductCategoryWithProductsQuery : IRequest<ProductCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
