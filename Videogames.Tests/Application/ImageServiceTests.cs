using Moq;
using Videogames.Application.Services;
using Videogames.Domain.Ports;
using Xunit;

namespace Videogames.Tests.Application;

public class ImageServiceTests
{
    private readonly Mock<IStoragePort> _storagePortMock;
    private readonly ImageService _service;

    public ImageServiceTests()
    {
        _storagePortMock = new Mock<IStoragePort>();
        _service = new ImageService(_storagePortMock.Object);
    }

    [Theory]
    [InlineData("image/jpeg", ".jpg")]
    [InlineData("image/png", ".png")]
    [InlineData("image/webp", ".webp")]
    [InlineData("application/pdf", "")]
    public async Task UploadImageAsync_ShouldGenerateGuidAndCorrectExtension(string contentType, string expectedExtension)
    {
        // Arrange
        var stream = new MemoryStream();
        _storagePortMock.Setup(s => s.UploadFileAsync(It.IsAny<Stream>(), It.IsAny<string>(), contentType))
            .ReturnsAsync((Stream s, string name, string type) => name);

        // Act
        var result = await _service.UploadImageAsync(stream, contentType);

        // Assert
        Assert.EndsWith(expectedExtension, result);
        var guidPart = !string.IsNullOrEmpty(expectedExtension) 
            ? result.Replace(expectedExtension, "") 
            : result;
        Assert.True(Guid.TryParse(guidPart, out _));
        
        _storagePortMock.Verify(s => s.UploadFileAsync(stream, It.IsAny<string>(), contentType), Times.Once);
    }

    [Fact]
    public async Task GetImageUrlAsync_ShouldCallStoragePort()
    {
        // Arrange
        var fileName = "test-image.jpg";
        var expectedUrl = "https://s3.example.com/videogames/test-image.jpg?token=123";
        
        _storagePortMock.Setup(s => s.GetFileUrlAsync(fileName))
            .ReturnsAsync(expectedUrl);

        // Act
        var result = await _service.GetImageUrlAsync(fileName);

        // Assert
        Assert.Equal(expectedUrl, result);
        _storagePortMock.Verify(s => s.GetFileUrlAsync(fileName), Times.Once);
    }
}
