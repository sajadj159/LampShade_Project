using DiscountManagement.Contracts;
using InventoryManagement.Contracts;
using LampShade.BuildingBlocks.Application;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Orders;

/// <summary>
/// Prices a cart on the server. Only product ids and counts are taken from the client; unit prices
/// come from Inventory and discount rates from Discount (colleague rates for colleague accounts).
/// </summary>
public sealed class CartCalculator(IInventoryModuleApi inventory, IDiscountModuleApi discounts, IAuthHelper authHelper)
{
    public async Task<Contract.Order.Cart> ComputeAsync(IEnumerable<CartItem> requestedItems, CancellationToken cancellationToken)
    {
        var items = (requestedItems ?? []).Where(x => x is { Id: > 0, Count: > 0 }).ToList();
        var stock = await inventory.GetStockAsync(items.Select(x => x.Id), cancellationToken);
        var rates = authHelper.CurrentAccountRole() == Roles.ColleagueUser
            ? await discounts.GetColleagueDiscountsAsync(cancellationToken)
            : await discounts.GetActiveCustomerDiscountsAsync(cancellationToken);

        var cart = new Contract.Order.Cart();
        foreach (var item in items)
        {
            var productStock = stock.GetValueOrDefault(item.Id);
            item.UnitPrice = productStock?.UnitPrice ?? 0;
            item.IsInStock = productStock is { InStock: true } && productStock.CurrentCount >= item.Count;
            item.DiscountRate = rates.FirstOrDefault(x => x.ProductId == item.Id)?.DiscountRate ?? 0;
            item.CalculateTotalItemPrice();
            item.DiscountAmount = item.TotalItemPrice * item.DiscountRate / 100;
            item.ItemPayAmount = item.TotalItemPrice - item.DiscountAmount;
            cart.Add(item);
        }

        return cart;
    }
}
