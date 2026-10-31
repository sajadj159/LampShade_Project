using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;

namespace DiscountManagement.Application.ReadModel.Discounts;

/// <summary>Admin-panel reads for discounts; implemented in Infrastructure against the discount schema.</summary>
public interface IDiscountQuery
{
    Task<EditCustomerDiscount> GetCustomerDiscountDetailsAsync(long id, CancellationToken cancellationToken = default);
    Task<List<CustomerDiscountViewmodel>> SearchCustomerDiscountsAsync(CostumerDiscountSearchModel searchModel, CancellationToken cancellationToken = default);
    Task<EditColleagueDiscount> GetColleagueDiscountDetailsAsync(long id, CancellationToken cancellationToken = default);
    Task<List<ColleagueDiscountViewModel>> SearchColleagueDiscountsAsync(ColleagueDiscountSearchModel searchModel, CancellationToken cancellationToken = default);
}
