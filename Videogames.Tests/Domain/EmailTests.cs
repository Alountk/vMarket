using Videogames.Domain.ValueObjects;
using Xunit;

namespace Videogames.Tests.Domain;

public class EmailTests
{
    [Fact]
    public void Create_ValidEmail_ReturnsEmailObject()
    {
        // Arrange
        var emailString = "test@example.com";

        // Act
        var email = Email.Create(emailString);

        // Assert
        Assert.NotNull(email);
        Assert.Equal("test@example.com", email.Value);
    }

    [Fact]
    public void Create_ValidEmail_NormalizesToLowerCase()
    {
        // Arrange
        var emailString = "Test@Example.COM";

        // Act
        var email = Email.Create(emailString);

        // Assert
        Assert.Equal("test@example.com", email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_EmptyOrNullEmail_ThrowsException(string? emailString)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Email.Create(emailString!));
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid@")]
    [InlineData("@example.com")]
    [InlineData("invalid@.com")]
    public void Create_InvalidEmailFormat_ThrowsException(string emailString)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Email.Create(emailString));
    }

    [Fact]
    public void Equals_SameEmail_ReturnsTrue()
    {
        // Arrange
        var email1 = Email.Create("test@example.com");
        var email2 = Email.Create("test@example.com");

        // Act & Assert
        Assert.Equal(email1, email2);
    }

    [Fact]
    public void Equals_DifferentCase_ReturnsTrue()
    {
        // Arrange
        var email1 = Email.Create("test@example.com");
        var email2 = Email.Create("TEST@EXAMPLE.COM");

        // Act & Assert
        Assert.Equal(email1, email2);
    }

    [Fact]
    public void Equals_DifferentEmail_ReturnsFalse()
    {
        // Arrange
        var email1 = Email.Create("test1@example.com");
        var email2 = Email.Create("test2@example.com");

        // Act & Assert
        Assert.NotEqual(email1, email2);
    }

    [Fact]
    public void ToString_ReturnsEmailValue()
    {
        // Arrange
        var email = Email.Create("test@example.com");

        // Act
        var result = email.ToString();

        // Assert
        Assert.Equal("test@example.com", result);
    }
}
