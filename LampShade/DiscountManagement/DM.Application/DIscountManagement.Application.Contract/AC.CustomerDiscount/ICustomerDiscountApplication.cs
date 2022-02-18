using System.Collections.Generic;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.AC.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        List<CustomerDiscountViewmodel> Search(CostumerDiscountSearchModel searchModel);
        EditCustomerDiscount GetDetails(long id);
    }
}