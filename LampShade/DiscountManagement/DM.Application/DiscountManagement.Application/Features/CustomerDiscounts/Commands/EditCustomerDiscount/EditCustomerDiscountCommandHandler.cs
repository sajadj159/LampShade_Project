using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.EditCustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.EditCustomerDiscount;

public class EditCustomerDiscountCommandHandler(ICustomerDiscountRepository discounts) : IRequestHandler<EditCustomerDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = discounts.Get(request.Id);
        if (discount is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (discounts.Exist(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate && x.Id != request.Id)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        discount.Edit(request.ProductId, request.DiscountRate, request.StartDate.ToGeorgianDateTime(), request.EndDate.ToGeorgianDateTime().Date.AddDays(1).AddTicks(-1), request.Reason); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
