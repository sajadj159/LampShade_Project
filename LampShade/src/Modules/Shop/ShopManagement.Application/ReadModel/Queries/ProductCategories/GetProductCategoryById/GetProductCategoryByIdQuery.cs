using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoryById;

public class GetProductCategoryByIdQuery : IRequest<EditProductCategory>
{
    public long Id { get; set; }
}
