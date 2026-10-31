using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.Contract.ProductCategory;

using ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoryById;

namespace ShopManagement.Application.ReadModel.Features.ProductCategories.Queries.GetProductCategoryById;



public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, EditProductCategory>
{
    private readonly IProductCategoryReadRepository _application;
    public GetProductCategoryByIdQueryHandler(IProductCategoryReadRepository application) => _application = application;
    public Task<EditProductCategory> Handle(GetProductCategoryByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
