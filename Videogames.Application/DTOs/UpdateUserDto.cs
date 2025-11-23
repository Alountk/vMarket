using System.ComponentModel.DataAnnotations;

namespace Videogames.Application.DTOs;

public record UpdateUserDto(
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    string? FirstName,

    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    string? LastName,

    [EmailAddress(ErrorMessage = "Invalid email format")]
    string? Email,

    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
    string? Password,

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    string? Address,

    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    string? City,

    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    string? Country,

    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    string? Phone
);
