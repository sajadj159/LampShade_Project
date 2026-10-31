using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;

public class IncreaseInventoryCommand : ICommand<OperationResult>
{
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
}
