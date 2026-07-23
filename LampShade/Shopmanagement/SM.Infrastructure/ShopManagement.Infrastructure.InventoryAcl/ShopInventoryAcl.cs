using System.Collections.Generic;
using _0_Framework.Application;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl;

public class ShopInventoryAcl(IInventoryRepository inventories, IAuthHelper authHelper) : IShopInventoryAcl
{
    public bool ReduceFromInventory(List<OrderItem> items)
    {
        foreach (var item in items) inventories.GetBy(item.ProductId).Reduce(item.Count, authHelper.CurrentAccountId(), "خرید مشتری", item.OrderId);
        inventories.Save();
        return true;
    }
}

