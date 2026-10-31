using _0_Framework.Application;
using _0_Framework.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LampShade.Api.Controllers.Write;

[Authorize(Roles = Roles.Administrator)]
[ApiController]
[Route("api/write/[controller]")]
public class EditorImageController : ControllerBase
{
    private const long MaximumImageSize = 5 * 1024 * 1024;
    private readonly IFIleUploader _uploader;

    public EditorImageController(IFIleUploader uploader) => _uploader = uploader;

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
