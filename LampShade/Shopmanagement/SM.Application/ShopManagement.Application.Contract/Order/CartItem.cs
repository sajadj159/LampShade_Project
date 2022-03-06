using System;

namespace ShopManagement.Application.Contract.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public int Count { get; set; }
        public double TotalItemPrice { get; set; }
        public bool IsInStock { get; set; }

        public CartItem()
        {
            TotalItemPrice = UnitPrice * Count;
        }

    }
}