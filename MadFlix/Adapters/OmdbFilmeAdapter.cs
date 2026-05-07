using MadFlix.Models;

namespace MadFlix.Adapters;

public static class OmdbFilmeAdapter
{
    public static Filme ToFilme(OmdbFilme omdb)
    {
        return new Filme
        {
            Titulo = omdb.Titulo,
            Ano = ParseAno(omdb.Ano),
            Genero = omdb.Genero,
            Sinopse = omdb.Sinopse,
            Poster = omdb.Poster,
            ImdbId = omdb.ImdbId,
            ImdbRating = omdb.ImdbRating,
            Runtime = omdb.Runtime,
            Rated = omdb.Rated
        };
    }

    private static int ParseAno(string ano)
    {
        // OMDb a veces devuelve "2010–2015" para series
        var anoLimpo = ano.Split('–')[0].Trim();
        return int.TryParse(anoLimpo, out var resultado) ? resultado : 0;
    }
}