using ShopManagement.Application.ReadModel.Product;
using MediatR;

namespace ShopManagement.Application.ReadModel.Queries.Products.GetLatestArrivals;

public class GetLatestArrivalsQuery : IRequest<List<ProductQueryModel>> { }
