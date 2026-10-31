using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository(DiscountContext context) : RepositoryBase<long, CustomerDiscount>(context), ICustomerDiscountRepository
    {
    }
}
