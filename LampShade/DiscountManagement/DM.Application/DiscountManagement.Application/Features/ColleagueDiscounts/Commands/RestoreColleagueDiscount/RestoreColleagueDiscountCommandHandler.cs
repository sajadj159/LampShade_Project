using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RestoreColleagueDiscount;

public class RestoreColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<RestoreColleagueDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RestoreColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = await discounts.GetAsync(request.Id, cancellationToken);
        if (discount is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        discount.Restore(); return operation.Succeeded();
    }
}
