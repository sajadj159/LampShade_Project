using System.Text.Json.Serialization;

namespace ShopManagement.Application.Contract.A.Slide
{
    public class EditSlide : CreateSlide
    {
        public long Id { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string SavedPictureUrl { get; set; }
    }
}
