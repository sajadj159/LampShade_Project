using InventoryManagement.Application.ReadModel.Inventory;
using MediatR;

namespace InventoryManagement.Application.ReadModel.Queries.Inventories.CheckStock;

public class CheckStockQuery : IRequest<StockStatus>
{
    public long ProductId { get; set; }
    public int Count { get; set; }
}
