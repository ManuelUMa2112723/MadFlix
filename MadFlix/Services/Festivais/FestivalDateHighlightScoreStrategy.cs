using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public sealed class FestivalDateHighlightScoreStrategy : IFestivalHighlightScoreStrategy
{
    public int Score(Festival festival, DateTime today)
    {
        if (festival.DataInicio.Date <= today && festival.DataFim.Date >= today)
            return 1_000;

        if (festival.DataInicio.Date > today)
        {
            var daysUntilStart = Math.Clamp((festival.DataInicio.Date - today).Days, 0, 90);
            return 500 - daysUntilStart;
        }

        var daysSinceEnd = Math.Clamp((today - festival.DataFim.Date).Days, 0, 90);
        return 100 - daysSinceEnd;
    }
}
