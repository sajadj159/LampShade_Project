using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug); 
        List<ProductQueryModel> Search(string value);
    }
}