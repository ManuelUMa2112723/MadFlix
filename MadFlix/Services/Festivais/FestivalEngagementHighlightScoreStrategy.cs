using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public sealed class FestivalEngagementHighlightScoreStrategy : IFestivalHighlightScoreStrategy
{
    public int Score(Festival festival, DateTime today)
    {
        var purchasesScore = festival.Compras.Count * 12;
        var votesScore = festival.VotosFilmes.Count * 8;
        var catalogScore = festival.Filmes.Count * 2;

        return purchasesScore + votesScore + catalogScore;
    }
}
