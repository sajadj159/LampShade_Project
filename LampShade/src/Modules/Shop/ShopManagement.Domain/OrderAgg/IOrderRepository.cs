using LampShade.BuildingBlocks.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
    }
}
