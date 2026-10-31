using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository(DiscountContext context) : RepositoryBase<long, ColleagueDiscount>(context), IColleagueDiscountRepository
    {
    }
}
