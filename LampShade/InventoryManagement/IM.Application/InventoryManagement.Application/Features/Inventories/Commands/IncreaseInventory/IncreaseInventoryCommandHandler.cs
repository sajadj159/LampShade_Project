using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.IncreaseInventory;

public class IncreaseInventoryCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<IncreaseInventoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = inventories.Get(request.InventoryId);
        if (inventory is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        inventory.Increase(request.Count, authHelper.CurrentAccountId(), request.Description); inventories.Save(); return Task.FromResult(operation.Succeeded());
    }
}
