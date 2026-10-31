using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoryById;

public class GetProductCategoryByIdQuery : IRequest<EditProductCategory>
{
    public long Id { get; set; }
}
