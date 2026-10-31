using Amazon.S3;
using Amazon.S3.Model;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LampShade.Api.Storage;

public sealed class S3FileUploader : IFIleUploader
{
    private readonly IAmazonS3 _s3Client;
    private readonly ObjectStorageOptions _options;

    public S3FileUploader(IAmazonS3 s3Client, IOptions<ObjectStorageOptions> options)
    {
        _s3Client = s3Client;
        _options = options.Value;
    }

    public string Upload(IFormFile file, string path)
    {
        if (file == null || file.Length == 0)
        {
            return string.Empty;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var prefix = string.Join('/', path
            .Replace('\\', '/')
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .Select(segment => segment.Trim()));
        var objectKey = string.IsNullOrWhiteSpace(prefix)
            ? $"{Guid.NewGuid():N}{extension}"
            : $"{prefix}/{Guid.NewGuid():N}{extension}";

        var request = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = objectKey,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        _s3Client.PutObjectAsync(request).GetAwaiter().GetResult();
        return objectKey;
    }
}
