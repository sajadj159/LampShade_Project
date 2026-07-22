using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;

public class ReduceInventoryRangeCommand : IRequest<OperationResult>
{
    public List<ReduceInventoryItem> Items { get; set; } = new();
}

public class ReduceInventoryItem
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
    public long OrderId { get; set; }
}
