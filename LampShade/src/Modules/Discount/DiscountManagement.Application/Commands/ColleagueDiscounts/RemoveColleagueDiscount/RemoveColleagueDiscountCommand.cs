using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RemoveColleagueDiscount;

public class RemoveColleagueDiscountCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
}
