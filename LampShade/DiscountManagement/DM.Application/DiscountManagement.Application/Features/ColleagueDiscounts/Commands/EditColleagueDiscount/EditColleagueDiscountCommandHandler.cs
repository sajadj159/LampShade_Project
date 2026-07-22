using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.EditColleagueDiscount;



public class EditColleagueDiscountCommandHandler : IRequestHandler<EditColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public EditColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.ColleagueDiscount.EditColleagueDiscount
        {
            Id = request.Id,
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate
        };
        return Task.FromResult(_application.Edit(command));
    }
}
