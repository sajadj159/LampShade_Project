namespace InventoryManagement.Contracts;

/// <summary>The Inventory module's public read API. Inventory is the source of truth for prices and stock.</summary>
public interface IInventoryModuleApi
{
    /// <summary>Price and stock of the given products, or of every product with an inventory when <paramref name="productIds"/> is null.</summary>
    Task<IReadOnlyDictionary<long, ProductStock>> GetStockAsync(IEnumerable<long> productIds = null, CancellationToken cancellationToken = default);
}

public sealed record ProductStock(long ProductId, double UnitPrice, bool InStock, long CurrentCount);
