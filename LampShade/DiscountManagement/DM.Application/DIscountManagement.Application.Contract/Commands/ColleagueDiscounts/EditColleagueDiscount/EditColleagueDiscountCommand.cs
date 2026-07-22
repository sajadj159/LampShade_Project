using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;

public class EditColleagueDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}
