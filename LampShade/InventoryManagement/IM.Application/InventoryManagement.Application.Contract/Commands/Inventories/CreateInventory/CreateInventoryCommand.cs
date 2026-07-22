using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;

public class CreateInventoryCommand : IRequest<OperationResult>
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}
