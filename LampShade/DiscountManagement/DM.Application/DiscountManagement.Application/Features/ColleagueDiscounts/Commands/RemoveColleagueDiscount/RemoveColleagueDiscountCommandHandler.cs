using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RemoveColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RemoveColleagueDiscount;

public class RemoveColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<RemoveColleagueDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = discounts.Get(request.Id);
        if (discount is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        discount.Remove(); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
