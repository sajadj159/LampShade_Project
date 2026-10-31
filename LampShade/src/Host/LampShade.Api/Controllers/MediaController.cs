using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LampShade.BuildingBlocks.Infrastructure;
using System.Net;

namespace LampShade.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly ObjectStorageOptions _options;

    public MediaController(IAmazonS3 s3Client, IOptions<ObjectStorageOptions> options)
    {
        _s3Client = s3Client;
        _options = options.Value;
    }

    [HttpGet("{**key}")]
    public async Task<IActionResult> Get(string key, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return NotFound();
        }

        try
        {
            var response = await _s3Client.GetObjectAsync(new GetObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key
            }, cancellationToken);

            // Only allow-listed image types are rendered inline; anything else (e.g. an object stored before
            // uploads were restricted) is sent as a download so the browser never runs it on this origin.
            Response.Headers.XContentTypeOptions = "nosniff";
            var contentType = response.Headers.ContentType;
            if (contentType is null || !S3FileUploader.ImageContentTypes.Values.Contains(contentType))
            {
                return File(response.ResponseStream, "application/octet-stream", Path.GetFileName(key));
            }

            return File(response.ResponseStream, contentType);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
    }
}
