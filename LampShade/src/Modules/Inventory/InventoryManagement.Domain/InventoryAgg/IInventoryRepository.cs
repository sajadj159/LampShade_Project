using LampShade.BuildingBlocks.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        Task<Inventory> GetByAsync(long productId, CancellationToken cancellationToken = default);
    }
}
