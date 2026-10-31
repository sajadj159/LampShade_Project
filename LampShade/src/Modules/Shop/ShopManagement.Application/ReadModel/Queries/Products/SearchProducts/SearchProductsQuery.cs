#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.SearchProducts;

public class SearchProductsQuery : IRequest<List<ProductViewModel>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? CategoryId { get; set; }
}
