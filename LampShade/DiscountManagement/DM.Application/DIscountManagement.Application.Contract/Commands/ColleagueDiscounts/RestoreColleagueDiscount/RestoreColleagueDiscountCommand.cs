using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;

public class RestoreColleagueDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}
