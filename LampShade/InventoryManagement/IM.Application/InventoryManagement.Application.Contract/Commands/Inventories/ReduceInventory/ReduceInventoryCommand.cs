using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;

public class ReduceInventoryCommand : IRequest<OperationResult>
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
    public long OrderId { get; set; }
}
