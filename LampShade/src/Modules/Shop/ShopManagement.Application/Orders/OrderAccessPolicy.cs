using LampShade.BuildingBlocks.Application;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application.Orders;

/// <summary>An order is visible to the customer who placed it and to administrators.</summary>
public sealed class OrderAccessPolicy(IOrderRepository orders, IAuthHelper authHelper)
{
    public bool IsAdministrator => authHelper.CurrentAccountRole() == Roles.Administrator;

    public long CurrentAccountId => authHelper.CurrentAccountId();

    public async Task<bool> CanAccessAsync(long orderId, CancellationToken cancellationToken)
    {
        if (IsAdministrator) return true;
        var order = await orders.GetAsync(orderId, cancellationToken);
        return order is not null && order.AccountId == CurrentAccountId;
    }

    public bool CanAccess(Domain.OrderAgg.Order order) => IsAdministrator || order.AccountId == CurrentAccountId;
}
