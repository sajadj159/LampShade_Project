using LampShade.BuildingBlocks.Application;

namespace ShopManagement.Application.Orders;

/// <summary>Shop's outbox: integration events added here are stored in the shop schema with the command's changes.</summary>
public interface IShopOutbox : IOutbox;
