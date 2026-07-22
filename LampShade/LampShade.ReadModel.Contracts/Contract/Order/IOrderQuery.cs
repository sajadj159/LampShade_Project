using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.Order
{
    public interface IOrderQuery
    {
        List<OrderQueryModel> GetPayedOrders();
        List<OrderQueryModel> GetOrders(long accountId);
    }
}