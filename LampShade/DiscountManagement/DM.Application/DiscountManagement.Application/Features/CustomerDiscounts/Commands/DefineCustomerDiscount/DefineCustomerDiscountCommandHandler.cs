using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.DefineCustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.DefineCustomerDiscount;

public class DefineCustomerDiscountCommandHandler(ICustomerDiscountRepository discounts) : IRequestHandler<DefineCustomerDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DefineCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await discounts.ExistAsync(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        discounts.Add(new CustomerDiscount(request.ProductId, request.DiscountRate, request.StartDate.ToGeorgianDateTime(), request.EndDate.ToGeorgianDateTime().Date.AddDays(1).AddTicks(-1), request.Reason)); return operation.Succeeded();
    }
}
