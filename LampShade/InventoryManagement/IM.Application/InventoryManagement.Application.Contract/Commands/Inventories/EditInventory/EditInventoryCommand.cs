using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;

public class EditInventoryCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}
