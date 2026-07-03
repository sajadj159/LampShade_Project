using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ColleagueDiscounts.Commands.RestoreColleagueDiscount;

public class RestoreColleagueDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RestoreColleagueDiscountCommandHandler : IRequestHandler<RestoreColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public RestoreColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(RestoreColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Restore(request.Id));
    }
}
