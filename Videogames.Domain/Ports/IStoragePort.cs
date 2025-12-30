namespace Videogames.Domain.Ports;

public interface IStoragePort
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
}
