using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using _0_Framework.Application;

using DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.EditCustomerDiscount;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Commands.EditCustomerDiscount;



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
