using System.Collections.Generic;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.AC.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
        EditColleagueDiscount GetDetails(long id);
    }
}