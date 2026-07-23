using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.EditColleagueDiscount;

public class EditColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<EditColleagueDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = await discounts.GetAsync(request.Id, cancellationToken);
        if (discount is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await discounts.ExistAsync(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        discount.Edit(request.ProductId, request.DiscountRate); return operation.Succeeded();
    }
}
