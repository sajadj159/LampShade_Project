using System.Collections.Generic;
using _0_Framework.Domain;
using InventoryManagement.Application.Contract.AC.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        EditInventory GetDetails(long id);
        Inventory GetBy(long productId);
    }
}