using LampShade.ReadModel.Contracts.Product;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Products.GetLatestArrivals;

public class GetLatestArrivalsQuery : IRequest<List<ProductQueryModel>> { }
