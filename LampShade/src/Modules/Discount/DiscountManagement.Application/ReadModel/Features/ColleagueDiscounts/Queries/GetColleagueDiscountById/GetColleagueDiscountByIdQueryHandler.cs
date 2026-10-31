using System.Threading;
using System.Threading.Tasks;
using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using DiscountManagement.Application.ReadModel.Discounts;

using DiscountManagement.Application.ReadModel.Queries.ColleagueDiscounts.GetColleagueDiscountById;

namespace DiscountManagement.Application.ReadModel.Features.ColleagueDiscounts.Queries.GetColleagueDiscountById;



public class GetColleagueDiscountByIdQueryHandler : IRequestHandler<GetColleagueDiscountByIdQuery, EditColleagueDiscount>
{
    private readonly IDiscountQuery _repository;

    public GetColleagueDiscountByIdQueryHandler(IDiscountQuery repository)
    {
        _repository = repository;
    }

    public Task<EditColleagueDiscount> Handle(GetColleagueDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetColleagueDiscountDetailsAsync(request.Id, cancellationToken);
    }
}
