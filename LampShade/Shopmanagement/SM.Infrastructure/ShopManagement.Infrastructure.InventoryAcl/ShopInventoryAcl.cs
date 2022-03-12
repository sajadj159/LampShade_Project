using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Application.Contract.AC.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = items.Select(x => new ReduceInventory(x.ProductId, x.Count, "خرید مشتری", x.OrderId)).ToList();

            return _inventoryApplication.Reduce(command).IsSucceeded;
        }
    }
}