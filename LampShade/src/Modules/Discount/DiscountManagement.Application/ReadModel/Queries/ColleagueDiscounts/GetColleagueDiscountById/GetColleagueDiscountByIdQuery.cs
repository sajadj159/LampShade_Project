using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ColleagueDiscounts.GetColleagueDiscountById;

public class GetColleagueDiscountByIdQuery : IRequest<EditColleagueDiscount>
{
    public long Id { get; set; }
}
