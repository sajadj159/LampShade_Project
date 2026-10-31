using LampShade.BuildingBlocks.Application;

namespace ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;

public class CancelOrderCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
