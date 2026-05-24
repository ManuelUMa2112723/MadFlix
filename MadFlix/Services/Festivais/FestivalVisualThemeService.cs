using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public sealed class FestivalVisualThemeService : IFestivalVisualThemeService
{
    private static readonly IReadOnlyDictionary<string, string> ImagesByGenre =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["acao"] = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?auto=format&fit=crop&w=1800&q=85",
            ["ação"] = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?auto=format&fit=crop&w=1800&q=85",
            ["terror"] = "https://images.unsplash.com/photo-1509248961158-e54f6934749c?auto=format&fit=crop&w=1800&q=85",
            ["drama"] = "https://images.unsplash.com/photo-1485846234645-a62644f84728?auto=format&fit=crop&w=1800&q=85",
            ["comedia"] = "https://images.unsplash.com/photo-1527224538127-2104bb71c51b?auto=format&fit=crop&w=1800&q=85",
            ["comédia"] = "https://images.unsplash.com/photo-1527224538127-2104bb71c51b?auto=format&fit=crop&w=1800&q=85",
            ["romance"] = "https://images.unsplash.com/photo-1517604931442-7e0c8ed2963c?auto=format&fit=crop&w=1800&q=85",
            ["sci-fi"] = "https://images.unsplash.com/photo-1534447677768-be436bb09401?auto=format&fit=crop&w=1800&q=85",
            ["ficcao"] = "https://images.unsplash.com/photo-1534447677768-be436bb09401?auto=format&fit=crop&w=1800&q=85",
            ["ficção"] = "https://images.unsplash.com/photo-1534447677768-be436bb09401?auto=format&fit=crop&w=1800&q=85",
            ["documentario"] = "https://images.unsplash.com/photo-1492691527719-9d1e07e534b4?auto=format&fit=crop&w=1800&q=85",
            ["documentário"] = "https://images.unsplash.com/photo-1492691527719-9d1e07e534b4?auto=format&fit=crop&w=1800&q=85"
        };

    private static readonly string[] FallbackImages =
    [
        "https://images.unsplash.com/photo-1505686994434-e3cc5abf1330?auto=format&fit=crop&w=1800&q=85",
        "https://images.unsplash.com/photo-1517604931442-7e0c8ed2963c?auto=format&fit=crop&w=1800&q=85",
        "https://images.unsplash.com/photo-1524985069026-dd778a71c7b4?auto=format&fit=crop&w=1800&q=85",
        "https://images.unsplash.com/photo-1503095396549-807759245b35?auto=format&fit=crop&w=1800&q=85"
    ];

    public string GetImageUrl(Festival festival)
    {
        var genre = festival.Genero.Trim();

        if (ImagesByGenre.TryGetValue(genre, out var imageUrl))
            return imageUrl;

        var index = Math.Abs(HashCode.Combine(festival.Id, festival.Nome, festival.Genero)) % FallbackImages.Length;
        return FallbackImages[index];
    }
}
