using System.ComponentModel.DataAnnotations;

namespace Videogames.Application.DTOs;

public record CreateUserDto(
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    string FirstName,

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    string LastName,

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
    string Password,

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    string Address,

    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    string City,

    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    string Country,

    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    string Phone
);
