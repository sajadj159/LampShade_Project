using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;

public sealed class PlaceOrderResult(Func<long> getOrderId) : ICommitAwareResponse
{
    public long OrderId { get; private set; }

    public void OnCommitted() => OrderId = getOrderId();
}