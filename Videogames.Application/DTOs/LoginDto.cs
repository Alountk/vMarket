using System.ComponentModel.DataAnnotations;

namespace Videogames.Application.DTOs;

public record LoginDto(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    string Password
);
