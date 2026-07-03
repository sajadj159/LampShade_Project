using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace LampShade.Api.Features.ProductCategories.Queries.GetProductCategoryById;

public class GetProductCategoryByIdQuery : IRequest<EditProductCategory>
{
    public long Id { get; set; }
}

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, EditProductCategory>
{
    private readonly IProductCategoryApplication _application;
    public GetProductCategoryByIdQueryHandler(IProductCategoryApplication application) => _application = application;
    public async Task<EditProductCategory> Handle(GetProductCategoryByIdQuery r, CancellationToken c) => await Task.FromResult(_application.GetDetails(r.Id));
}
