using Amazon.S3;
using Amazon.S3.Model;
using LampShade.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LampShade.BuildingBlocks.Infrastructure;

public sealed class S3FileUploader : IFileUploader
{
    private readonly IAmazonS3 _s3Client;
    private readonly ObjectStorageOptions _options;

    public S3FileUploader(IAmazonS3 s3Client, IOptions<ObjectStorageOptions> options)
    {
        _s3Client = s3Client;
        _options = options.Value;
    }

    public const long MaximumFileSize = 5 * 1024 * 1024;

    public static readonly IReadOnlyDictionary<string, string> ImageContentTypes = new Dictionary<string, string>
    {
        [".jpg"] = "image/jpeg",
        [".jpeg"] = "image/jpeg",
        [".png"] = "image/png",
        [".webp"] = "image/webp",
        [".gif"] = "image/gif"
    };

    public string Upload(IFormFile file, string path)
    {
        if (file == null || file.Length == 0)
        {
            return string.Empty;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!ImageContentTypes.TryGetValue(extension, out var contentType))
        {
            throw new AppValidationException("Only JPG, PNG, WEBP and GIF images can be uploaded.");
        }

        if (file.Length > MaximumFileSize)
        {
            throw new AppValidationException("Uploaded images must be 5 MB or smaller.");
        }

        var prefix = string.Join('/', path
            .Replace('\\', '/')
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select(segment => segment.Trim())
            .Where(segment => segment is not "." and not ".."));
        var objectKey = string.IsNullOrWhiteSpace(prefix)
            ? $"{Guid.NewGuid():N}{extension}"
            : $"{prefix}/{Guid.NewGuid():N}{extension}";

        var request = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = objectKey,
            InputStream = file.OpenReadStream(),
            // Never trust the client's content type: it decides how the browser renders the file later.
            ContentType = contentType
        };

        _s3Client.PutObjectAsync(request).GetAwaiter().GetResult();
        return objectKey;
    }
}
