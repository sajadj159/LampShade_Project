using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ColleagueDiscounts.Commands.EditColleagueDiscount;

public class EditColleagueDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
}

public class EditColleagueDiscountCommandHandler : IRequestHandler<EditColleagueDiscountCommand, OperationResult>
{
    private readonly IColleagueDiscountApplication _application;

    public EditColleagueDiscountCommandHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(EditColleagueDiscountCommand request, CancellationToken cancellationToken)
    {
        var command = new DiscountManagement.Application.Contract.AC.ColleagueDiscount.EditColleagueDiscount
        {
            Id = request.Id,
            ProductId = request.ProductId,
            DiscountRate = request.DiscountRate
        };
        return await Task.FromResult(_application.Edit(command));
    }
}
