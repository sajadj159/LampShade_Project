using LampShade.BuildingBlocks.Application;

namespace ShopManagement.Contracts.IntegrationEvents;

/// <summary>
/// Published by Shop when an order is confirmed as paid (approved payment proof or approved cash on delivery).
/// Inventory takes the items out of stock; Account notifies the buyer.
/// </summary>
public sealed record OrderPaidIntegrationEvent(
    long OrderId,
    long AccountId,
    string IssueTrackingNumber,
    long ApprovedBy,
    IReadOnlyList<OrderPaidItem> Items) : IntegrationEvent;

public sealed record OrderPaidItem(long ProductId, int Count);
