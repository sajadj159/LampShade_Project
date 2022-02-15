namespace ShopManagement.Application.Contract.A.ProductPicture
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string Product { get; set; }
        public string PictureUrl { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}