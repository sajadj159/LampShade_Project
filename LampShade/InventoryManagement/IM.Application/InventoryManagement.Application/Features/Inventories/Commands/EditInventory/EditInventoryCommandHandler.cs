using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.EditInventory;

public class EditInventoryCommandHandler(IInventoryRepository inventories) : IRequestHandler<EditInventoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = await inventories.GetAsync(request.Id, cancellationToken);
        if (inventory is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await inventories.ExistAsync(x => x.ProductId == request.ProductId && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        inventory.Edit(request.ProductId, request.UnitPrice); return operation.Succeeded();
    }
}
