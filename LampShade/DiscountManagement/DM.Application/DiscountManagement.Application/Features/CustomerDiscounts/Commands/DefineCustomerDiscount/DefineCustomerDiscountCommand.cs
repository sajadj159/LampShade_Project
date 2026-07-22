using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using _0_Framework.Application;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.DefineCustomerDiscount;

public class DefineCustomerDiscountCommand : IRequest<OperationResult>
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

public class DefineCustomerDiscountCommandHandler : IRequestHandler<DefineCustomerDiscountCommand, OperationResult>
{
    private readonly ICustomerDiscountApplication _application;

    public DefineCustomerDiscountCommandHandler(ICustomerDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(DefineCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.CustomerDiscount.DefineCustomerDiscount
        {
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason
        };
        return Task.FromResult(_application.Define(command));
    }
}
