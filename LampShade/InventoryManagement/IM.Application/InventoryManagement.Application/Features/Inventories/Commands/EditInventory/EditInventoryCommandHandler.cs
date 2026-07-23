using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.EditInventory;

public class EditInventoryCommandHandler(IInventoryRepository inventories) : IRequestHandler<EditInventoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var inventory = inventories.Get(request.Id);
        if (inventory is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (inventories.Exist(x => x.ProductId == request.ProductId && x.Id != request.Id)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        inventory.Edit(request.ProductId, request.UnitPrice); inventories.Save(); return Task.FromResult(operation.Succeeded());
    }
}
