using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;
using InventoryManagement.Application.Contract.AC.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        EditInventory GetDetails(long id);
        Inventory GetBy(long productId);
        Task<Inventory> GetByAsync(long productId, CancellationToken cancellationToken = default);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
