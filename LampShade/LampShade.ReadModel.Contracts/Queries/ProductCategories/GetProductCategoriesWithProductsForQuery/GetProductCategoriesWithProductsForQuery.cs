using MediatR;
using LampShade.ReadModel.Contracts.ProductCategory;

namespace LampShade.ReadModel.Contracts.Queries.ProductCategories.GetProductCategoriesWithProductsForQuery;

public class GetProductCategoriesWithProductsForQuery : IRequest<List<ProductCategoryQueryModel>> { }
