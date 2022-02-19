using System.Collections.Generic;
using System.Linq;
using _0_Framework.Repository;
using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var queryable = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                InStock = x.InStock,
                UnitPrice = x.UnitPrice,
                CurrentCount = x.CalculateCurrentCount()
            });
            if (!searchModel.InStock)
            {
                queryable = queryable.Where(x => !x.InStock);
            }

            if (searchModel.ProductId > 0)
            {
                queryable = queryable.Where(x => x.ProductId == searchModel.ProductId);
            }

            var inventory = queryable.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
            return inventory;
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }
    }
}