using LampShade.ReadModel.Contracts.Inventory;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Inventories.CheckStock;

namespace LampShade.ReadModel.Application.Features.Inventories.Queries.CheckStock;



public class CheckStockQueryHandler : IRequestHandler<CheckStockQuery, StockStatus>
{
    private readonly IInventoryQuery _inventoryQuery;

    public CheckStockQueryHandler(IInventoryQuery inventoryQuery)
    {
        _inventoryQuery = inventoryQuery;
    }

    public Task<StockStatus> Handle(CheckStockQuery request, CancellationToken cancellationToken)
    {
        var command = new IsInStock
        {
            ProductId = request.ProductId,
            Count = request.Count
        };
        return Task.FromResult(_inventoryQuery.CheckStock(command));
    }
}
