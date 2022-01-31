using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        List<ProductCategory> GetAll();
        ProductCategory Get(long id);
        bool Exist(Expression<Func<ProductCategory, bool>> expression);
        void Save();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}