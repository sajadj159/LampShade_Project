using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventoryRange;

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

public class ReduceInventoryRangeCommandHandler : IRequestHandler<ReduceInventoryRangeCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public ReduceInventoryRangeCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(ReduceInventoryRangeCommand request, CancellationToken cancellationToken)
    {
        var commands = request.Items.Select(i => new InventoryManagement.Application.Contract.AC.Inventory.ReduceInventory
        {
            InventoryId = i.InventoryId,
            ProductId = i.ProductId,
            Count = i.Count,
            Description = i.Description,
            OrderId = i.OrderId
        }).ToList();
        return Task.FromResult(_application.Reduce(commands));
    }
}
