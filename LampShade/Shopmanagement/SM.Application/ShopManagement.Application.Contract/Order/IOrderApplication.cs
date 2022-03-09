namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
       long PlaceOrder(Cart cart); 
    }
}