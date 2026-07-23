using _0_Framework.Application;
using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.DefineColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.DefineColleagueDiscount;

public class DefineColleagueDiscountCommandHandler(IColleagueDiscountRepository discounts) : IRequestHandler<DefineColleagueDiscountCommand, OperationResult>
{
    public Task<OperationResult> Handle(DefineColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); if (discounts.Exist(x => x.ProductId == request.ProductId && x.DiscountRate == request.DiscountRate)) return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));
        discounts.Create(new ColleagueDiscount(request.ProductId, request.DiscountRate)); discounts.Save(); return Task.FromResult(operation.Succeeded());
    }
}
