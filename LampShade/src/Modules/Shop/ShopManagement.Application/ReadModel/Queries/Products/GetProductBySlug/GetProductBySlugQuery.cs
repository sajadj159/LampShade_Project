using LampShade.ReadModel.Contracts.Product;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Products.GetProductBySlug;

public class GetProductBySlugQuery : IRequest<ProductQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
