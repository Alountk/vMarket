using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using Videogames.Domain.Ports;
using Videogames.Infrastructure.Configuration;

namespace Videogames.Infrastructure.Adapters;

public class MinioStorageAdapter : IStoragePort
{
    private readonly MinioSettings _settings;
    private readonly IAmazonS3 _s3Client;

    public MinioStorageAdapter(IOptions<MinioSettings> settings)
    {
        _settings = settings.Value;

        var config = new AmazonS3Config
        {
            ServiceURL = _settings.Endpoint,
            ForcePathStyle = true, // Required for MinIO
            UseHttp = !_settings.UseSSL
        };

        _s3Client = new AmazonS3Client(_settings.AccessKey, _settings.SecretKey, config);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var fileTransferUtility = new TransferUtility(_s3Client);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = fileName,
            BucketName = _settings.BucketName,
            ContentType = contentType
        };

        await fileTransferUtility.UploadAsync(uploadRequest);

        // For MinIO, we return the constructed URL or just the fileName depending on how the frontend should access it.
        // Usually, in Hexagonal, we return the identifier/path.
        return fileName;
    }
}
