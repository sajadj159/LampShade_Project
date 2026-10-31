using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventory;

public class ReduceInventoryCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<ReduceInventoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ReduceInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = await inventories.GetAsync(request.InventoryId, cancellationToken);
        if (inventory is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        inventory.Reduce(request.Count, authHelper.CurrentAccountId(), request.Description, request.OrderId); return operation.Succeeded();
    }
}
