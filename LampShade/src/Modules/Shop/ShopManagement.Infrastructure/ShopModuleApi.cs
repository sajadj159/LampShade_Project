using Microsoft.EntityFrameworkCore;
using ShopManagement.Contracts;
using ShopManagement.Infrastructure.EFCore;

namespace ShopManagement.Infrastructure;

internal sealed class ShopModuleApi(ShopContext context) : IShopModuleApi
{
    public async Task<IReadOnlyDictionary<long, string>> GetProductNamesAsync(IEnumerable<long> productIds, CancellationToken cancellationToken = default)
    {
        var ids = productIds.Distinct().ToList();
        return await context.Products.AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
    }
}
