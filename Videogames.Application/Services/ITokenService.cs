using Videogames.Domain.Entities;

namespace Videogames.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
