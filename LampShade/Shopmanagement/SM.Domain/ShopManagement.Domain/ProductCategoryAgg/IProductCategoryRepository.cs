﻿using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        string GetSlugBy(long id); 
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}