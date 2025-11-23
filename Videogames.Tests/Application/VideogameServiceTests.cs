using Moq;
using Videogames.Application.DTOs;
using Videogames.Application.Services;
using Videogames.Domain.Entities;
using Videogames.Domain.Enums;
using Videogames.Domain.Ports;
using Videogames.Domain.ValueObjects;
using Xunit;

namespace Videogames.Tests.Application;

public class VideogameServiceTests
{
    private readonly Mock<IVideogameRepository> _repositoryMock;
    private readonly VideogameService _service;

    public VideogameServiceTests()
    {
        _repositoryMock = new Mock<IVideogameRepository>();
        _service = new VideogameService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedVideogame()
    {
        // Arrange
        var createDto = new CreateVideogameDto(
            "The Legend of Zelda",
            new List<LocalizedName> { new("Zelda", "es") },
            "qr-code",
            "123456",
            "Switch",
            new List<string> { "asset1" },
            new List<string> { "image1" },
            GameState.Released,
            DateTime.Now,
            "1.0.0"
        );

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Videogame>()))
            .ReturnsAsync((Videogame v) => v);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.EnglishName, result.EnglishName);
        Assert.Equal(createDto.Console, result.Console);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Videogame>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnVideogame_WhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var videogame = new Videogame { Id = id, EnglishName = "Test Game" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(videogame);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Test Game", result.EnglishName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Videogame?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
    }
}
