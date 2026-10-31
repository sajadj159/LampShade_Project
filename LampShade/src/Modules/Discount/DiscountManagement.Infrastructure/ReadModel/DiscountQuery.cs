using System.Globalization;
using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using DiscountManagement.Application.ReadModel.Discounts;
using DiscountManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Contracts;

namespace DiscountManagement.Infrastructure.ReadModel;

internal sealed class DiscountQuery(DiscountContext context, IShopModuleApi shop) : IDiscountQuery
{
    public async Task<EditCustomerDiscount> GetCustomerDiscountDetailsAsync(long id, CancellationToken cancellationToken = default)
    {
        var discount = await context.CustomerDiscounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return discount is null
            ? null
            : new EditCustomerDiscount
            {
                Id = discount.Id,
                DiscountRate = discount.DiscountRate,
                ProductId = discount.ProductId,
                Reason = discount.Reason,
                EndDate = discount.EndDate.ToString(CultureInfo.InvariantCulture),
                StartDate = discount.StartDate.ToString(CultureInfo.InvariantCulture)
            };
    }

    public async Task<List<CustomerDiscountViewmodel>> SearchCustomerDiscountsAsync(CostumerDiscountSearchModel searchModel, CancellationToken cancellationToken = default)
    {
        var query = context.CustomerDiscounts.AsNoTracking();
        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
        {
            var startDate = searchModel.StartDate.ToGeorgianDateTime();
            query = query.Where(x => x.StartDate >= startDate);
        }
        if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
        {
            var endDate = searchModel.EndDate.ToGeorgianDateTime();
            query = query.Where(x => x.EndDate <= endDate);
        }

        var discounts = (await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken))
            .Select(x => new CustomerDiscountViewmodel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();

        var productNames = await shop.GetProductNamesAsync(discounts.Select(x => x.ProductId), cancellationToken);
        discounts.ForEach(discount => discount.Product = productNames.GetValueOrDefault(discount.ProductId));
        return discounts;
    }

    public Task<EditColleagueDiscount> GetColleagueDiscountDetailsAsync(long id, CancellationToken cancellationToken = default) =>
        context.ColleagueDiscounts.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new EditColleagueDiscount { Id = x.Id, ProductId = x.ProductId, DiscountRate = x.DiscountRate })
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<List<ColleagueDiscountViewModel>> SearchColleagueDiscountsAsync(ColleagueDiscountSearchModel searchModel, CancellationToken cancellationToken = default)
    {
        var query = context.ColleagueDiscounts.AsNoTracking();
        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId && !x.IsRemoved);

        var discounts = (await query.OrderByDescending(x => x.Id).ToListAsync(cancellationToken))
            .Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                DiscountRate = x.DiscountRate,
                IsRemoved = x.IsRemoved
            }).ToList();

        var productNames = await shop.GetProductNamesAsync(discounts.Select(x => x.ProductId), cancellationToken);
        discounts.ForEach(discount => discount.Product = productNames.GetValueOrDefault(discount.ProductId));
        return discounts;
    }
}
