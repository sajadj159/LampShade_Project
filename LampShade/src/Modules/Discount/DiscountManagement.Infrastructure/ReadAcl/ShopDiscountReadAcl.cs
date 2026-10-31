using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Acl;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore.ReadAcl;

public sealed class ShopDiscountReadAcl(DiscountContext context) : IShopDiscountReadAcl
{
    public Task<List<DiscountReadModel>> GetCustomerDiscountsAsync(bool onlyActive, CancellationToken cancellationToken = default)
    {
        var query = context.CustomerDiscounts.AsNoTracking().AsQueryable();
        if (onlyActive) query = query.Where(item => item.StartDate < DateTime.UtcNow && item.EndDate > DateTime.UtcNow);
        return query.Select(item => new DiscountReadModel(item.ProductId, item.DiscountRate, item.EndDate)).ToListAsync(cancellationToken);
    }

    public List<DiscountReadModel> GetCustomerDiscounts(bool onlyActive)
    {
        var query = context.CustomerDiscounts.AsNoTracking().AsQueryable();
        if (onlyActive) query = query.Where(item => item.StartDate < DateTime.UtcNow && item.EndDate > DateTime.UtcNow);
        return query.Select(item => new DiscountReadModel(item.ProductId, item.DiscountRate, item.EndDate)).ToList();
    }
    public List<DiscountReadModel> GetColleagueDiscounts() => context.ColleagueDiscounts.AsNoTracking().Where(item => !item.IsRemoved).Select(item => new DiscountReadModel(item.ProductId, item.DiscountRate, DateTime.MaxValue)).ToList();
}