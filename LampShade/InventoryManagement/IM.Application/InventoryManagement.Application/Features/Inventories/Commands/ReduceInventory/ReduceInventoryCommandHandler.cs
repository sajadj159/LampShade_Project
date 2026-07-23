using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventory;

public class ReduceInventoryCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<ReduceInventoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = inventories.Get(request.InventoryId);
        if (inventory is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        inventory.Reduce(request.Count, authHelper.CurrentAccountId(), request.Description, request.OrderId); inventories.Save(); return Task.FromResult(operation.Succeeded());
    }
}
