using System.Collections.Generic;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Contract.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug); 
        List<ProductQueryModel> Search(string value);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}