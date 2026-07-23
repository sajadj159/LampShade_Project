using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.CreateInventory;

public class CreateInventoryCommandHandler(IInventoryRepository inventories) : IRequestHandler<CreateInventoryCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await inventories.ExistAsync(x => x.ProductId == request.ProductId, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        inventories.Add(new Inventory(request.ProductId, request.UnitPrice));
        return operation.Succeeded();
    }
}
