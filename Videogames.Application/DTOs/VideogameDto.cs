using Videogames.Domain.Enums;
using Videogames.Domain.ValueObjects;

namespace Videogames.Application.DTOs;

public record VideogameDto(
    Guid Id,
    string EnglishName,
    List<LocalizedName> Names,
    string Qr,
    string Codebar,
    string Console,
    List<string> Assets,
    List<string> Images,
    GameState State,
    DateTime ReleaseDate,
    string VersionGame
);
