using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventoryRange;

public class ReduceInventoryRangeCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<ReduceInventoryRangeCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ReduceInventoryRangeCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            var inventory = await inventories.GetByAsync(item.ProductId, cancellationToken);
            if (inventory is null) return new OperationResult().Failed(ApplicationMessages.RecordNotFound);
            inventory.Reduce(item.Count, authHelper.CurrentAccountId(), item.Description, item.OrderId);
        }
        return new OperationResult().Succeeded();
    }
}
