using InventoryManagement.Application.Contract.AC.Inventory;

namespace InventoryManagement.Application.ReadModel.Inventory;

public interface IInventoryQuery
{
    Task<StockStatus> CheckStockAsync(IsInStock command, CancellationToken cancellationToken = default);
    Task<List<InventoryViewModel>> SearchAsync(InventorySearchModel searchModel, CancellationToken cancellationToken = default);
    Task<EditInventory> GetDetailsAsync(long id, CancellationToken cancellationToken = default);
    Task<List<InventoryOperationViewModel>> GetOperationLogAsync(long inventoryId, CancellationToken cancellationToken = default);
}
