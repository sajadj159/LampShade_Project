using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.DefineColleagueDiscount;

public class DefineColleagueDiscountCommand : ICommand<OperationResult>
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}
