using ShopManagement.Application.ReadModel.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.SearchProductsForQuery;

public class SearchProductsForQuery : IRequest<List<ProductQueryModel>>
{
    public string Value { get; set; } = string.Empty;
}
