using System.Linq;
using _01_LampShadeQuery.Contract.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        public InventoryQuery(InventoryContext context, ShopContext shopContext)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _context.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var products = _shopContext.Products.Select(x => new { x.Name, x.Id }).FirstOrDefault(x => x.Id == command.ProductId);

                return new StockStatus
                {
                    IsStock = false,
                    ProductName = products?.Name
                };
            }

            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}