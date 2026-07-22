using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RemoveColleagueDiscount;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RemoveColleagueDiscount;



public class RemoveColleagueDiscountCommandHandler : IRequestHandler<RemoveColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public RemoveColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Remove(request.Id));
    }
}
