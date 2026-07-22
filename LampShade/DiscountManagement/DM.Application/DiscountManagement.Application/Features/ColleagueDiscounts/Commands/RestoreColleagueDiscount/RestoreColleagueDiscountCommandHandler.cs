using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

using DiscountManagement.Application.Contracts.Commands.ColleagueDiscounts.RestoreColleagueDiscount;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Commands.RestoreColleagueDiscount;



public class RestoreColleagueDiscountCommandHandler : IRequestHandler<RestoreColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public RestoreColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(RestoreColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Restore(request.Id));
    }
}
