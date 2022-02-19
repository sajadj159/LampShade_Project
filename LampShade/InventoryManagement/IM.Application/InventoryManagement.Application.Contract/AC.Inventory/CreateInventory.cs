using System.Collections.Generic;
using ShopManagement.Application.Contract.A.Product;

namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}