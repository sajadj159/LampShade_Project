using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long,CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewmodel> Search(CostumerDiscountSearchModel searchModel);
    }
}