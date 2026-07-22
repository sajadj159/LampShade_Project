using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace DiscountManagement.Application.Features.ColleagueDiscounts.Queries.GetColleagueDiscountById;

public class GetColleagueDiscountByIdQuery : IRequest<EditColleagueDiscount>
{
    public long Id { get; set; }
}

public class GetColleagueDiscountByIdQueryHandler : IRequestHandler<GetColleagueDiscountByIdQuery, EditColleagueDiscount>
{
    private readonly IColleagueDiscountApplication _application;

    public GetColleagueDiscountByIdQueryHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public Task<EditColleagueDiscount> Handle(GetColleagueDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
