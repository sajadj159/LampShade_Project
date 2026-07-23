using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Commands.ReduceInventoryRange;

public class ReduceInventoryRangeCommandHandler(IInventoryRepository inventories, IAuthHelper authHelper) : IRequestHandler<ReduceInventoryRangeCommand, OperationResult>
{
    public Task<OperationResult> Handle(ReduceInventoryRangeCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
            inventories.GetBy(item.ProductId).Reduce(item.Count, authHelper.CurrentAccountId(), item.Description, item.OrderId);
        inventories.Save(); return Task.FromResult(new OperationResult().Succeeded());
    }
}
