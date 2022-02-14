﻿using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.A.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);

        OperationResult InStock(long id);
        OperationResult NotInStock(long id);
    }
}