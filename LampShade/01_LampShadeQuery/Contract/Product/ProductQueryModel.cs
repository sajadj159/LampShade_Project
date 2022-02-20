namespace _01_LampShadeQuery.Contract.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string PictureUrl { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }

    }
}