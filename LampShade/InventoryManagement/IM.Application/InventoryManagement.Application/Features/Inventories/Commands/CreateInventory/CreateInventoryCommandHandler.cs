using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.CreateInventory;

public class CreateInventoryCommandHandler(IInventoryRepository inventories) : IRequestHandler<CreateInventoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (inventories.Exist(x => x.ProductId == request.ProductId)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        inventories.Create(new Inventory(request.ProductId, request.UnitPrice)); inventories.Save();
        return Task.FromResult(operation.Succeeded());
    }
}
