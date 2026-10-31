using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace DiscountManagement.Application.ReadModel.Queries.ColleagueDiscounts.GetColleagueDiscountById;

public class GetColleagueDiscountByIdQuery : IRequest<EditColleagueDiscount>
{
    public long Id { get; set; }
}
