using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventory;



public class ReduceInventoryCommandHandler : IRequestHandler<ReduceInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public ReduceInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.ReduceInventory
        {
            InventoryId = request.InventoryId,
            ProductId = request.ProductId,
            Count = request.Count,
            Description = request.Description,
            OrderId = request.OrderId
        };
        return Task.FromResult(_application.Reduce(command));
    }
}
