using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public sealed class HomeFestivalHighlights
{
    public Festival? FeaturedFestival { get; init; }
    public IReadOnlyList<Festival> NowShowingFestivals { get; init; } = [];
}
