namespace LampShade.ReadModel.Contracts.Order;

public interface IOrderQuery
{
    Task<List<OrderQueryModel>> GetPayedOrdersAsync(CancellationToken cancellationToken = default);
    Task<List<OrderQueryModel>> GetOrdersAsync(long accountId, CancellationToken cancellationToken = default);
}