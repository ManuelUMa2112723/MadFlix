namespace MadFlix.Services.Festivais;

public interface IFestivalHomeService
{
    Task<HomeFestivalHighlights> GetHighlightsAsync(CancellationToken cancellationToken = default);
}
