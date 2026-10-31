using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.IncreaseInventory;

public class IncreaseInventoryCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<IncreaseInventoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = await inventories.GetAsync(request.InventoryId, cancellationToken);
        if (inventory is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        inventory.Increase(request.Count, authHelper.CurrentAccountId(), request.Description); return operation.Succeeded();
    }
}
