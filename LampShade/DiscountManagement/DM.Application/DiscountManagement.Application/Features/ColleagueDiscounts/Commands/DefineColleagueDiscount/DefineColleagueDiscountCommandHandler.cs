using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.DefineColleagueDiscount;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.DefineColleagueDiscount;



public class DefineColleagueDiscountCommandHandler : IRequestHandler<DefineColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public DefineColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(DefineColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.ColleagueDiscount.DefineColleagueDiscount
        {
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate
        };
        return Task.FromResult(_application.Define(command));
    }
}
