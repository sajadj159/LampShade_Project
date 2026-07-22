using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using _0_Framework.Application;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.EditCustomerDiscount;

public class EditCustomerDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

public class EditCustomerDiscountCommandHandler : IRequestHandler<EditCustomerDiscountCommand, OperationResult>
{
    private readonly ICustomerDiscountApplication _application;

    public EditCustomerDiscountCommandHandler(ICustomerDiscountApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditCustomerDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.CustomerDiscount.EditCustomerDiscount
        {
            Id = request.Id,
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason
        };
        return Task.FromResult(_application.Edit(command));
    }
}
