using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Inventories.Commands.ReduceInventory;

public class ReduceInventoryCommand : IRequest<OperationResult>
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
    public long OrderId { get; set; }
}

public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public ReduceInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.ReduceInventory
        {
            InventoryId = request.InventoryId,
            ProductId = request.ProductId,
            Count = request.Count,
            Description = request.Description,
            OrderId = request.OrderId
        };
        return await Task.FromResult(_application.Reduce(command));
    }
}
