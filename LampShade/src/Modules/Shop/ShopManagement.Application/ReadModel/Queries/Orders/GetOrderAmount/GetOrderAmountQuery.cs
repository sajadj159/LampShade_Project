using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.ReadModel.Queries.Orders.GetOrderAmount;

public class GetOrderAmountQuery : IRequest<double>
{
    public long Id { get; set; }
}
