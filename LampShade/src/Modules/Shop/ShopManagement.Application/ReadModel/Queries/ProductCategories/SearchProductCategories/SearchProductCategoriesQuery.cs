#nullable enable

using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.ReadModel.Queries.ProductCategories.SearchProductCategories;

public class SearchProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>>
{
    public string? Name { get; set; }
}
