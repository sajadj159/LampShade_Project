using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Products.GetProductById;

public class GetProductByIdQuery : IRequest<EditProduct>
{
    public long Id { get; set; }
}
