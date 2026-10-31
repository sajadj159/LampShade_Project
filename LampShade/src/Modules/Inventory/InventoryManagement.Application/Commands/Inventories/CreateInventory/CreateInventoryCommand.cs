using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;

public class CreateInventoryCommand : ICommand<OperationResult>
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}
