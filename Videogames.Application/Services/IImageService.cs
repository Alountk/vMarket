namespace Videogames.Application.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(Stream fileStream, string contentType);
    Task<Stream> GetImageAsync(string fileName);
    Task<string> GetImageUrlAsync(string fileName);
}
