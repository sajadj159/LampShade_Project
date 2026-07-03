using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ColleagueDiscounts.Commands.RemoveColleagueDiscount;

public class RemoveColleagueDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
}

public class RemoveColleagueDiscountCommandHandler : IRequestHandler<RemoveColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public RemoveColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(RemoveColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Remove(request.Id));
    }
}
