using Videogames.Application.Security;
using Xunit;

namespace Videogames.Tests.Application;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_ValidPassword_ReturnsHashedPassword()
    {
        // Arrange
        var password = "SecurePassword123";

        // Act
        var hashedPassword = PasswordHasher.HashPassword(password);

        // Assert
        Assert.NotNull(hashedPassword);
        Assert.NotEqual(password, hashedPassword);
        Assert.StartsWith("$2", hashedPassword);
    }

    [Fact]
    public void HashPassword_SamePassword_ReturnsDifferentHashes()
    {
        // Arrange
        var password = "SecurePassword123";

        // Act
        var hash1 = PasswordHasher.HashPassword(password);
        var hash2 = PasswordHasher.HashPassword(password);

        // Assert
        Assert.NotEqual(hash1, hash2); // BCrypt uses salt, so hashes should differ
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "SecurePassword123";
        var hashedPassword = PasswordHasher.HashPassword(password);

        // Act
        var result = PasswordHasher.VerifyPassword(password, hashedPassword);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_IncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var password = "SecurePassword123";
        var wrongPassword = "WrongPassword456";
        var hashedPassword = PasswordHasher.HashPassword(password);

        // Act
        var result = PasswordHasher.VerifyPassword(wrongPassword, hashedPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HashPassword_EmptyPassword_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => PasswordHasher.HashPassword(""));
        Assert.Throws<ArgumentException>(() => PasswordHasher.HashPassword("   "));
    }

    [Fact]
    public void VerifyPassword_EmptyPassword_ReturnsFalse()
    {
        // Arrange
        var hashedPassword = PasswordHasher.HashPassword("ValidPassword");

        // Act
        var result = PasswordHasher.VerifyPassword("", hashedPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_EmptyHash_ReturnsFalse()
    {
        // Act
        var result = PasswordHasher.VerifyPassword("ValidPassword", "");

        // Assert
        Assert.False(result);
    }
}
