using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RemoveColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RemoveColleagueDiscount;

public class RemoveColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<RemoveColleagueDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = await discounts.GetAsync(request.Id, cancellationToken);
        if (discount is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        discount.Remove(); return operation.Succeeded();
    }
}
