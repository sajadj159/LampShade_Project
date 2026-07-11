using System.Text.Json.Serialization;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public class EditProductCategory : CreateProductCategory
    {
        public long  Id { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string SavedPictureUrl { get; set; }
    }
}
