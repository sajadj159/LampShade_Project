#nullable enable

using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace LampShade.ReadModel.Contracts.Queries.ProductCategories.SearchProductCategories;

public class SearchProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>>
{
    public string? Name { get; set; }
}
