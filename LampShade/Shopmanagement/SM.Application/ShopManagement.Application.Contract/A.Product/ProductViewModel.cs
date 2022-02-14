﻿namespace ShopManagement.Application.Contract.A.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Code { get; set; }
        public double UnitPrice { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
    }
}