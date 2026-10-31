using LampShade.BuildingBlocks.Application;
using AccountManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Contracts.IntegrationEvents;

namespace AccountManagement.Infrastructure.IntegrationEvents;

/// <summary>Account owns the buyer's name and mobile, so it is the module that tells them their order is on its way.</summary>
internal sealed class NotifyBuyerWhenOrderPaid(AccountContext context, ISmsService smsService)
    : IdempotentIntegrationEventHandler<OrderPaidIntegrationEvent, AccountContext>(context)
{
    protected override async Task HandleCoreAsync(OrderPaidIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var account = await context.Accounts.AsNoTracking()
            .Where(x => x.Id == integrationEvent.AccountId)
            .Select(x => new { x.FullName, x.Mobile })
            .FirstOrDefaultAsync(cancellationToken);
        if (account is null || string.IsNullOrWhiteSpace(account.Mobile))
            return;

        smsService.Send(account.Mobile, $"{account.FullName} گرامی سفارش شما با شماره پیگیری {integrationEvent.IssueTrackingNumber} تایید شد و ارسال خواهد شد.");
    }
}
