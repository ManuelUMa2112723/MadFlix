using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalHomeService : IFestivalHomeService
{
    private const int MaxNowShowingFestivals = 12;
    private readonly IFestivalRepository _festivalRepository;
    private readonly IEnumerable<IFestivalHighlightScoreStrategy> _scoreStrategies;

    public FestivalHomeService(
        IFestivalRepository festivalRepository,
        IEnumerable<IFestivalHighlightScoreStrategy> scoreStrategies)
    {
        _festivalRepository = festivalRepository;
        _scoreStrategies = scoreStrategies;
    }

    public async Task<HomeFestivalHighlights> GetHighlightsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var festivals = await _festivalRepository.ListarComMetricasAsync(cancellationToken);

        var orderedFestivals = festivals
            .OrderByDescending(festival => GetScore(festival, today))
            .ThenBy(festival => festival.DataInicio.Date < today ? today : festival.DataInicio.Date)
            .ThenBy(festival => festival.Nome)
            .ToList();

        var nowShowing = orderedFestivals
            .Where(festival => festival.DataInicio.Date <= today && festival.DataFim.Date >= today)
            .Take(MaxNowShowingFestivals)
            .ToList();

        if (nowShowing.Count == 0)
        {
            nowShowing = orderedFestivals
                .Where(festival => festival.DataFim.Date >= today)
                .Take(MaxNowShowingFestivals)
                .ToList();
        }

        if (nowShowing.Count == 0)
            nowShowing = orderedFestivals.Take(MaxNowShowingFestivals).ToList();

        return new HomeFestivalHighlights
        {
            FeaturedFestival = orderedFestivals.FirstOrDefault(),
            NowShowingFestivals = nowShowing
        };
    }

    private int GetScore(Festival festival, DateTime today)
    {
        return _scoreStrategies.Sum(strategy => strategy.Score(festival, today));
    }
}
