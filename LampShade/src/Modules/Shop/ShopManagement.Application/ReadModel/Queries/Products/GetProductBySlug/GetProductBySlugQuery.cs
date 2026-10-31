using ShopManagement.Application.ReadModel.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.GetProductBySlug;

public class GetProductBySlugQuery : IRequest<ProductQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
