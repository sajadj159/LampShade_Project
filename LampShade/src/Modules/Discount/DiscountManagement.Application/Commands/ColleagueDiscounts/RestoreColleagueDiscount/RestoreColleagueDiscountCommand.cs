using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;

public class RestoreColleagueDiscountCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
