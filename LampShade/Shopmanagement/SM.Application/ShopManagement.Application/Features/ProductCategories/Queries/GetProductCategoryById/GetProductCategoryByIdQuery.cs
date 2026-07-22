using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Features.ProductCategories.Queries.GetProductCategoryById;

public class GetProductCategoryByIdQuery : IRequest<EditProductCategory>
{
    public long Id { get; set; }
}

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, EditProductCategory>
{
    private readonly IProductCategoryApplication _application;
    public GetProductCategoryByIdQueryHandler(IProductCategoryApplication application) => _application = application;
    public Task<EditProductCategory> Handle(GetProductCategoryByIdQuery r, CancellationToken c) => Task.FromResult(_application.GetDetails(r.Id));
}
