using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Products.GetProducts;

public class GetProductsQuery : IRequest<List<ProductViewModel>> { }
