using LampShade.BuildingBlocks.Application;
using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.ApproveCashOnDelivery;

public class ApproveCashOnDeliveryCommand : ICommand<string>
{
    public long OrderId { get; set; }
}
