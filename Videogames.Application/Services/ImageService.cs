using Videogames.Domain.Ports;

namespace Videogames.Application.Services;

public class ImageService : IImageService
{
    private readonly IStoragePort _storagePort;

    public ImageService(IStoragePort storagePort)
    {
        _storagePort = storagePort;
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string contentType)
    {
        var fileName = $"{Guid.NewGuid()}{GetExtension(contentType)}";
        return await _storagePort.UploadFileAsync(fileStream, fileName, contentType);
    }

    public async Task<Stream> GetImageAsync(string fileName)
    {
        return await _storagePort.GetFileAsync(fileName);
    }

    public async Task<string> GetImageUrlAsync(string fileName)
    {
        return await _storagePort.GetFileUrlAsync(fileName);
    }

    private string GetExtension(string contentType)
    {
        return contentType.ToLower() switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "image/webp" => ".webp",
            _ => string.Empty
        };
    }
}
