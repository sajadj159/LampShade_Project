using MediatR;
using ShopManagement.Application.ReadModel.ProductCategory;

namespace ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoryWithProducts;

public class GetProductCategoryWithProductsQuery : IRequest<ProductCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
