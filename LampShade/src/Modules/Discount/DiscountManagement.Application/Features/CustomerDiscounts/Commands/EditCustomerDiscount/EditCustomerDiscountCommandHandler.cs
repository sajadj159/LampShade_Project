using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.EditCustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.EditCustomerDiscount;

public class EditCustomerDiscountCommandHandler(ICustomerDiscountRepository discounts) : IRequestHandler<EditCustomerDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var discount = await discounts.GetAsync(request.Id, cancellationToken);
        if (discount is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await discounts.ExistAsync(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate && x.Id != request.Id, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        discount.Edit(request.ProductId, request.DiscountRate, request.StartDate.ToGeorgianDateTime(), request.EndDate.ToGeorgianDateTime().Date.AddDays(1).AddTicks(-1), request.Reason); return operation.Succeeded();
    }
}
