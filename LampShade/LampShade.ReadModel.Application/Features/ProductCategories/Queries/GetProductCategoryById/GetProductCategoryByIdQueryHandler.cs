using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

using LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoryById;

namespace LampShade.ReadModel.Application.Features.ProductCategories.Queries.GetProductCategoryById;



public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, EditProductCategory>
{
    private readonly IProductCategoryApplication _application;
    public GetProductCategoryByIdQueryHandler(IProductCategoryApplication application) => _application = application;
    public Task<EditProductCategory> Handle(GetProductCategoryByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
