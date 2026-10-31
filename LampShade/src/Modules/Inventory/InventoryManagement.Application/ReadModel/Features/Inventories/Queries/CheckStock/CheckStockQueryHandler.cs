using System.Threading;
using System.Threading.Tasks;
using InventoryManagement.Application.ReadModel.Inventory;
using MediatR;

using InventoryManagement.Application.ReadModel.Queries.Inventories.CheckStock;

namespace InventoryManagement.Application.ReadModel.Features.Inventories.Queries.CheckStock;



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
        return _inventoryQuery.CheckStockAsync(command, cancellationToken);
    }
}
