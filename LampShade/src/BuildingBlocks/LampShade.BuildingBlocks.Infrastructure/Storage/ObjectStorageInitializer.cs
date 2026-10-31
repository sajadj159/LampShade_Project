using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace LampShade.Api.Storage;

public sealed class ObjectStorageInitializer : IHostedService
{
    private readonly IAmazonS3 _s3Client;
    private readonly ObjectStorageOptions _options;
    private readonly ILogger<ObjectStorageInitializer> _logger;

    public ObjectStorageInitializer(
        IAmazonS3 s3Client,
        IOptions<ObjectStorageOptions> options,
        ILogger<ObjectStorageInitializer> logger)
    {
        _s3Client = s3Client;
        _options = options.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (var attempt = 1; attempt <= 10; attempt++)
        {
            try
            {
                var buckets = await _s3Client.ListBucketsAsync(cancellationToken);
                if (!buckets.Buckets.Any(bucket => bucket.BucketName == _options.BucketName))
                {
                    await _s3Client.PutBucketAsync(new PutBucketRequest
                    {
                        BucketName = _options.BucketName
                    }, cancellationToken);
                }

                _logger.LogInformation("Object storage bucket {BucketName} is ready", _options.BucketName);
                return;
            }
            catch (Exception ex) when (attempt < 10)
            {
                _logger.LogWarning(ex, "Object storage is not ready. Retrying ({Attempt}/10)", attempt);
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        throw new InvalidOperationException("Object storage could not be initialized.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
