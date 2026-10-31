using ShopManagement.Application.Contract.A.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.GetProducts;

public class GetProductsQuery : IRequest<List<ProductViewModel>> { }
