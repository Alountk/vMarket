namespace Videogames.Application.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(Stream fileStream, string contentType);
}
