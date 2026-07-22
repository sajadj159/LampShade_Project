using LampShade.ReadModel.Contracts.Inventory;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Inventories.CheckStock;

public class CheckStockQuery : IRequest<StockStatus>
{
    public long ProductId { get; set; }
    public int Count { get; set; }
}
