namespace LampShade.ReadModel.Contracts.Inventory;

public interface IInventoryQuery
{
    Task<StockStatus> CheckStockAsync(IsInStock command, CancellationToken cancellationToken = default);
}