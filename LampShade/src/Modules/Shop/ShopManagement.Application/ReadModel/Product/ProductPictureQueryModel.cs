namespace LampShade.ReadModel.Contracts.Product
{
    public class ProductPictureQueryModel
    {
        public string PictureUrl { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public long ProductId { get; set; }
        public bool IsRemoved { get; set; }
    }
}