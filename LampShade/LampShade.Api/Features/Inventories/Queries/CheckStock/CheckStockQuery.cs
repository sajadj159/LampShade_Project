using _01_LampShadeQuery.Contract.Inventory;
using MediatR;

namespace LampShade.Api.Features.Inventories.Queries.CheckStock;

public class CheckStockQuery : IRequest<StockStatus>
{
    public long ProductId { get; set; }
    public int Count { get; set; }
}

public class CheckStockQueryHandler : IRequestHandler<CheckStockQuery, StockStatus>
{
    private readonly IInventoryQuery _inventoryQuery;

    public CheckStockQueryHandler(IInventoryQuery inventoryQuery)
    {
        _inventoryQuery = inventoryQuery;
    }

    public async Task<StockStatus> Handle(CheckStockQuery request, CancellationToken cancellationToken)
    {
        var command = new IsInStock
        {
            ProductId = request.ProductId,
            Count = request.Count
        };
        return await Task.FromResult(_inventoryQuery.CheckStock(command));
    }
}
