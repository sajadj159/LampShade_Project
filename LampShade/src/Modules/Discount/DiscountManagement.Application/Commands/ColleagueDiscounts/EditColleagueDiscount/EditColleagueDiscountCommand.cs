using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;

public class EditColleagueDiscountCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}
