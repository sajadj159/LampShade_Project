namespace ShopManagement.Contracts;

/// <summary>The Shop module's public read API for other modules.</summary>
public interface IShopModuleApi
{
    /// <summary>Product names by id; unknown ids are left out.</summary>
    Task<IReadOnlyDictionary<long, string>> GetProductNamesAsync(IEnumerable<long> productIds, CancellationToken cancellationToken = default);
}
