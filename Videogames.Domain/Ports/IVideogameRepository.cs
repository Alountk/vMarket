using Videogames.Domain.Entities;

namespace Videogames.Domain.Ports;

public interface IVideogameRepository
{
    Task<Videogame> CreateAsync(Videogame videogame);
    Task<Videogame?> GetByIdAsync(Guid id);
    Task<IEnumerable<Videogame>> GetAllAsync();
    Task UpdateAsync(Videogame videogame);
    Task DeleteAsync(Guid id);
}
