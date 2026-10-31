using InventoryManagement.Contracts;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

internal sealed class InventoryModuleApi(InventoryContext context) : IInventoryModuleApi
{
    public async Task<IReadOnlyDictionary<long, ProductStock>> GetStockAsync(IEnumerable<long> productIds = null, CancellationToken cancellationToken = default)
    {
        var query = context.Inventory.AsNoTracking();
        if (productIds is not null)
        {
            var ids = productIds.Distinct().ToList();
            query = query.Where(x => ids.Contains(x.ProductId));
        }

        return await query
            .Select(x => new ProductStock(
                x.ProductId,
                x.UnitPrice,
                x.InStock,
                x.InventoryOperations.Where(o => o.Operation).Sum(o => o.Count) - x.InventoryOperations.Where(o => !o.Operation).Sum(o => o.Count)))
            .ToDictionaryAsync(x => x.ProductId, cancellationToken);
    }
}
