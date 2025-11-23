using Videogames.Domain.Enums;
using Videogames.Domain.ValueObjects;

namespace Videogames.Domain.Entities;

public class Videogame
{
    public Guid Id { get; set; }
    public string EnglishName { get; set; } = string.Empty;
    public List<LocalizedName> Names { get; set; } = new();
    public string Qr { get; set; } = string.Empty;
    public string Codebar { get; set; } = string.Empty;
    public string Console { get; set; } = string.Empty;
    public List<string> Assets { get; set; } = new();
    public List<string> Images { get; set; } = new();
    public GameState State { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string VersionGame { get; set; } = string.Empty;
}
