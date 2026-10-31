using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;

public class EditInventoryCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}
