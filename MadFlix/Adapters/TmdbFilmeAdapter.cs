using MadFlix.Models;
using System.Linq;

namespace MadFlix.Adapters;

public static class TmdbFilmeAdapter
{
    private const string PosterBaseUrl = "https://image.tmdb.org/t/p/w500";

    public static Filme ToFilme(TmdbFilme tmdb)
    {
        return new Filme
        {
            TmdbId = tmdb.TmdbId,
            Titulo = tmdb.Titulo,
            Ano = ParseAno(tmdb.DataLancamento),
            Genero = string.Join(", ", tmdb.Generos.Select(g => g.Nome)),
            Sinopse = tmdb.Sinopse,
            Poster = string.IsNullOrEmpty(tmdb.PosterPath)
                ? null
                : $"{PosterBaseUrl}{tmdb.PosterPath}",
            ImdbId = tmdb.ImdbId,
            ImdbRating = tmdb.ImdbRating,
            Runtime = tmdb.Runtime > 0 ? $"{tmdb.Runtime} min" : null,
            Rated = tmdb.Adult ? "Adult" : "All audiences"
        };
    }

    private static int ParseAno(string dataLancamento)
    {
        if (string.IsNullOrEmpty(dataLancamento))
            return 0;

        return int.TryParse(dataLancamento.Split('-')[0], out var ano) ? ano : 0;
    }
}