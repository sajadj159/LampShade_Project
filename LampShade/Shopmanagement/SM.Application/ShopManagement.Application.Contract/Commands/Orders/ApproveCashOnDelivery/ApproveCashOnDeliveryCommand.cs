using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommand : IRequest<string>
{
    public long OrderId { get; set; }
}
