namespace Videogames.Application.Services;

using Videogames.Application.DTOs;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto createDto);
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<UserDto?> GetByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task UpdateAsync(Guid id, UpdateUserDto updateDto);
    Task DeleteAsync(Guid id);
}
