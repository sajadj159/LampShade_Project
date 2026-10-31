using DiscountManagement.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure;

internal sealed class DiscountModuleApi(DiscountContext context) : IDiscountModuleApi
{
    public Task<List<ProductDiscount>> GetActiveCustomerDiscountsAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return context.CustomerDiscounts.AsNoTracking()
            .Where(x => x.StartDate < now && x.EndDate > now)
            .Select(x => new ProductDiscount(x.ProductId, x.DiscountRate, x.EndDate))
            .ToListAsync(cancellationToken);
    }

    public Task<List<ProductDiscount>> GetColleagueDiscountsAsync(CancellationToken cancellationToken = default) =>
        context.ColleagueDiscounts.AsNoTracking()
            .Where(x => !x.IsRemoved)
            .Select(x => new ProductDiscount(x.ProductId, x.DiscountRate, DateTime.MaxValue))
            .ToListAsync(cancellationToken);
}
