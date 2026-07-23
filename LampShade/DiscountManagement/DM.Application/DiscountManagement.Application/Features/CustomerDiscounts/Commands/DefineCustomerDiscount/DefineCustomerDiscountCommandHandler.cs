using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.DefineCustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.DefineCustomerDiscount;

public class DefineCustomerDiscountCommandHandler(ICustomerDiscountRepository discounts) : IRequestHandler<DefineCustomerDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(DefineCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (discounts.Exist(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        discounts.Create(new CustomerDiscount(request.ProductId, request.DiscountRate, request.StartDate.ToGeorgianDateTime(), request.EndDate.ToGeorgianDateTime().Date.AddDays(1).AddTicks(-1), request.Reason)); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
