using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.DefineColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.DefineColleagueDiscount;

public class DefineColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<DefineColleagueDiscountCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DefineColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (await discounts.ExistAsync(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate, cancellationToken)) return operation.Failed(ApplicationMessages.DuplicatedRecord);
        discounts.Add(new ColleagueDiscount(request.ProductId, request.DiscountRate)); return operation.Succeeded();
    }
}
