using LampShade.ReadModel.Contracts.Product;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Products.SearchProductsForQuery;

public class SearchProductsForQuery : IRequest<List<ProductQueryModel>>
{
    public string Value { get; set; } = string.Empty;
}
