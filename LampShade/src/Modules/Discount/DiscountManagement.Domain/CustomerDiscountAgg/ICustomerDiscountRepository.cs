using LampShade.BuildingBlocks.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long,CustomerDiscount>
    {
    }
}
