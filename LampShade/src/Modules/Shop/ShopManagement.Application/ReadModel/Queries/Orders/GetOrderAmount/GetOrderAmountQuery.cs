using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Contracts.Queries.Orders.GetOrderAmount;

public class GetOrderAmountQuery : IRequest<double>
{
    public long Id { get; set; }
}
