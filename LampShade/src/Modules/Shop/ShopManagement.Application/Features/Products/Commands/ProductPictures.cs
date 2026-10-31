using LampShade.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Features.Products.Commands;

internal static class ProductPictures
{
    /// <summary>Uploads the gallery images sent with a product form and attaches them to the product.</summary>
    public static void AddAdditional(Domain.ProductAgg.Product product, IEnumerable<IFormFile> pictures, string path, IFileUploader uploader)
    {
        if (pictures is null) return;

        foreach (var picture in pictures.Where(x => x is { Length: > 0 }))
        {
            var picturePath = uploader.Upload(picture, path);
            var title = Path.GetFileNameWithoutExtension(picture.FileName);
            product.AddPicture(picturePath, title, title);
        }
    }
}
