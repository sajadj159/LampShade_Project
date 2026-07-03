using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ColleagueDiscounts.Commands.DefineColleagueDiscount;

public class DefineColleagueDiscountCommand : IRequest<OperationResult>
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}

public class DefineColleagueDiscountCommandHandler : IRequestHandler<DefineColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public DefineColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(DefineColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.ColleagueDiscount.DefineColleagueDiscount
        {
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate
        };
        return await Task.FromResult(_application.Define(command));
    }
}
