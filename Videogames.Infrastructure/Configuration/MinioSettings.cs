namespace Videogames.Infrastructure.Configuration;

public class MinioSettings
{
    public const string SectionName = "Minio";
    public string Endpoint { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string BucketName { get; set; } = "videogames";
    public bool UseSSL { get; set; }
}
