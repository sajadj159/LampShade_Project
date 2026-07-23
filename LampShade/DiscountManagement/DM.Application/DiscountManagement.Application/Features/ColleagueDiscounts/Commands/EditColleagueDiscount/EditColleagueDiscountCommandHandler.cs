using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.EditColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.EditColleagueDiscount;

public class EditColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<EditColleagueDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = discounts.Get(request.Id);
        if (discount is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (discounts.Exist(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate && x.Id != request.Id)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        discount.Edit(request.ProductId, request.DiscountRate); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
