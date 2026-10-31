using MediatR;
using ShopManagement.Application.ReadModel.ProductCategory;

namespace ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoriesWithProductsForQuery;

public class GetProductCategoriesWithProductsForQuery : IRequest<List<ProductCategoryQueryModel>> { }
