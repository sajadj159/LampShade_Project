using InventoryManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Contracts.IntegrationEvents;

namespace InventoryManagement.Infrastructure.IntegrationEvents;

/// <summary>Takes a paid order's items out of stock. Shop checked availability before confirming the order.</summary>
internal sealed class ReduceStockWhenOrderPaid(InventoryContext context)
    : IdempotentIntegrationEventHandler<OrderPaidIntegrationEvent, InventoryContext>(context)
{
    private const string Description = "خرید مشتری";

    protected override async Task HandleCoreAsync(OrderPaidIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var productIds = integrationEvent.Items.Select(x => x.ProductId).ToList();
        var inventories = await context.Inventory.Where(x => productIds.Contains(x.ProductId)).ToListAsync(cancellationToken);

        foreach (var item in integrationEvent.Items)
        {
            var inventory = inventories.FirstOrDefault(x => x.ProductId == item.ProductId)
                ?? throw new InvalidOperationException($"Order {integrationEvent.OrderId} contains product {item.ProductId}, which has no inventory.");
            inventory.Reduce(item.Count, integrationEvent.ApprovedBy, Description, integrationEvent.OrderId);
        }
    }
}
