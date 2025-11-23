using Videogames.Application.DTOs;
using Videogames.Domain.Entities;
using Videogames.Domain.Ports;

namespace Videogames.Application.Services;

public class VideogameService : IVideogameService
{
    private readonly IVideogameRepository _repository;

    public VideogameService(IVideogameRepository repository)
    {
        _repository = repository;
    }

    public async Task<VideogameDto> CreateAsync(CreateVideogameDto createDto)
    {
        var videogame = new Videogame
        {
            Id = Guid.NewGuid(),
            EnglishName = createDto.EnglishName,
            Names = createDto.Names,
            Qr = createDto.Qr,
            Codebar = createDto.Codebar,
            Console = createDto.Console,
            Assets = createDto.Assets,
            Images = createDto.Images,
            State = createDto.State,
            ReleaseDate = createDto.ReleaseDate,
            VersionGame = createDto.VersionGame
        };

        var created = await _repository.CreateAsync(videogame);
        return MapToDto(created);
    }

    public async Task<VideogameDto?> GetByIdAsync(Guid id)
    {
        var videogame = await _repository.GetByIdAsync(id);
        return videogame == null ? null : MapToDto(videogame);
    }

    public async Task<IEnumerable<VideogameDto>> GetAllAsync()
    {
        var videogames = await _repository.GetAllAsync();
        return videogames.Select(MapToDto);
    }

    public async Task UpdateAsync(Guid id, UpdateVideogameDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            // In a real app, we might throw a NotFoundException here
            return;
        }

        existing.EnglishName = updateDto.EnglishName;
        existing.Names = updateDto.Names;
        existing.Qr = updateDto.Qr;
        existing.Codebar = updateDto.Codebar;
        existing.Console = updateDto.Console;
        existing.Assets = updateDto.Assets;
        existing.Images = updateDto.Images;
        existing.State = updateDto.State;
        existing.ReleaseDate = updateDto.ReleaseDate;
        existing.VersionGame = updateDto.VersionGame;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    private static VideogameDto MapToDto(Videogame videogame)
    {
        return new VideogameDto(
            videogame.Id,
            videogame.EnglishName,
            videogame.Names,
            videogame.Qr,
            videogame.Codebar,
            videogame.Console,
            videogame.Assets,
            videogame.Images,
            videogame.State,
            videogame.ReleaseDate,
            videogame.VersionGame
        );
    }
}
