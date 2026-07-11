using System.Text.Json.Serialization;

namespace ShopManagement.Application.Contract.A.Product
{
    public class EditProduct : CreateProduct
    {
        public long Id { get; set; }
        public bool ClearMainPicture { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string SavedPictureUrl { get; set; }
    }
}
