using _0_Framework.Application;
using LampShade.ReadModel.Contracts.Order;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query;

public class OrderQuery(ShopContext shopContext) : IOrderQuery
{
    public Task<List<OrderQueryModel>> GetPayedOrdersAsync(CancellationToken cancellationToken = default) =>
        shopContext.Orders.AsNoTracking().Where(x => x.IsPaid).Select(x => new OrderQueryModel { Id = x.Id, PaymentMethodId = x.PaymentMethod, PayAmount = x.PayAmount }).ToListAsync(cancellationToken);

    public Task<List<OrderQueryModel>> GetOrdersAsync(long accountId, CancellationToken cancellationToken = default) =>
        shopContext.Orders.AsNoTracking().Where(x => x.AccountId == accountId).Select(x => new OrderQueryModel { Id = x.Id, PaymentMethodId = x.PaymentMethod, PayAmount = x.PayAmount, IsPaid = x.IsPaid, IsCanceled = x.IsCanceled, AccountId = x.AccountId, TotalAmount = x.TotalAmount, DiscountAmount = x.DiscountAmount, IssueTrackingNo = x.IssueTrackingNumber, PaymentProofUrl = x.PaymentProofUrl, PayDate = x.CreationDate.ToFarsi() }).ToListAsync(cancellationToken);
}