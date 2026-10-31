using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository(InventoryContext context) : RepositoryBase<long, Inventory>(context), IInventoryRepository
    {
        public Task<Inventory> GetByAsync(long productId, CancellationToken cancellationToken = default) =>
            context.Inventory.FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }
}
