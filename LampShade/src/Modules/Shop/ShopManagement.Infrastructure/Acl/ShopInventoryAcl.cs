using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl;

public class ShopInventoryAcl(IInventoryRepository inventories, IAuthHelper authHelper) : IShopInventoryAcl
{
    public async Task<bool> ReduceFromInventoryAsync(List<OrderItem> items, CancellationToken cancellationToken = default)
    {
        foreach (var item in items)
        {
            var inventory = await inventories.GetByAsync(item.ProductId, cancellationToken);
            if (inventory is null) return false;
            inventory.Reduce(item.Count, authHelper.CurrentAccountId(), "خرید مشتری", item.OrderId);
        }
        return true;
    }
}
