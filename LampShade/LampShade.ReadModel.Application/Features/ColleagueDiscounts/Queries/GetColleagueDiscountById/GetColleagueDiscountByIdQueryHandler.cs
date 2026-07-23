using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using DiscountManagement.Domain.ColleagueDiscountAgg;

using LampShade.ReadModel.Contracts.Queries.ColleagueDiscounts.GetColleagueDiscountById;

namespace LampShade.ReadModel.Application.Features.ColleagueDiscounts.Queries.GetColleagueDiscountById;



public class GetColleagueDiscountByIdQueryHandler : IRequestHandler<GetColleagueDiscountByIdQuery, EditColleagueDiscount>
{
    private readonly IColleagueDiscountRepository _repository;

    public GetColleagueDiscountByIdQueryHandler(IColleagueDiscountRepository repository)
    {
        _repository = repository;
    }

    public Task<EditColleagueDiscount> Handle(GetColleagueDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetDetails(request.Id));
    }
}

