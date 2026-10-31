using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/write/[controller]")]
public class EditorImageController : ControllerBase
{
    private const long MaximumImageSize = 5 * 1024 * 1024;
    private readonly IFileUploader _uploader;

    public EditorImageController(IFileUploader uploader) => _uploader = uploader;

    [HttpPost]
    public IActionResult Upload([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0 || !file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("An image file is required.");
        }

        if (file.Length > MaximumImageSize)
        {
            return BadRequest("Image size must be 5 MB or smaller.");
        }

        return Ok(new { key = _uploader.Upload(file, "Products/Description") });
    }
}
