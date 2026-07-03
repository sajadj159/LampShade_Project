using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace LampShade.Api.Features.ColleagueDiscounts.Queries.GetColleagueDiscountById;

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

    public async Task<EditColleagueDiscount> Handle(GetColleagueDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.GetDetails(request.Id));
    }
}
