using MediatR;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategories;

public class GetProductCategoriesQuery : IRequest<List<ProductCategoryViewModel>> { }
