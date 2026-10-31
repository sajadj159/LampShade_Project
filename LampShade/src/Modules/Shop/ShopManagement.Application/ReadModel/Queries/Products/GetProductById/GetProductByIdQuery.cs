using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.GetProductById;

public class GetProductByIdQuery : IRequest<EditProduct>
{
    public long Id { get; set; }
}
