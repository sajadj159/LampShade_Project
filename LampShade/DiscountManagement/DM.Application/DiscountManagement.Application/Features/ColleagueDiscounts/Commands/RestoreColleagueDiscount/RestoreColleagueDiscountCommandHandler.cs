using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RestoreColleagueDiscount;

public class RestoreColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<RestoreColleagueDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(RestoreColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = discounts.Get(request.Id);
        if (discount is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        discount.Restore(); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
