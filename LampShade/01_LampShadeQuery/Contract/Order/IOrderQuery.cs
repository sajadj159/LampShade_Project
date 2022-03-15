using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Order
{
    public interface IOrderQuery
    {
        List<OrderQueryModel> GetPayedOrders();
    }
}