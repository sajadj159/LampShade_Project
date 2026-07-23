using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategories;

public class GetProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>> { }
