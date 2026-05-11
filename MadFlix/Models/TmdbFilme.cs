using System.Text.Json.Serialization;

namespace MadFlix.Models;

public class TmdbFilme
{
    [JsonPropertyName("id")]
    public int TmdbId { get; set; }

    [JsonPropertyName("title")]
    public string Titulo { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string DataLancamento { get; set; } = string.Empty;

    [JsonPropertyName("genres")]
    public List<TmdbGenero> Generos { get; set; } = new();

    [JsonPropertyName("overview")]
    public string Sinopse { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; } = string.Empty;

    [JsonPropertyName("imdb_id")]
    public string ImdbId { get; set; } = string.Empty;

    [JsonPropertyName("vote_average")]
    public float ImdbRating { get; set; }

    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }
}

public class TmdbGenero
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; } = string.Empty;
}



public class TmdbCredits
{
    [JsonPropertyName("cast")]
    public List<TmdbCastMember> Cast { get; set; } = new();

    [JsonPropertyName("crew")]
    public List<TmdbCrewMember> Crew { get; set; } = new();
}

public class TmdbCastMember
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; } = string.Empty;
}

public class TmdbCrewMember
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("job")]
    public string Job { get; set; } = string.Empty;
}

public class TmdbSearchResult
{
    [JsonPropertyName("results")]
    public List<TmdbSearchItem> Results { get; set; } = new();
}

public class TmdbSearchItem
{
    [JsonPropertyName("id")]
    public int TmdbId { get; set; }

    [JsonPropertyName("title")]
    public string Titulo { get; set; } = string.Empty;
}


public class TmdbPersonSearchResult
{
    [JsonPropertyName("results")]
    public List<TmdbPersonItem> Results { get; set; } = new();
}

public class TmdbPersonItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nome { get; set; } = string.Empty;
}

public class TmdbPersonCredits
{
    [JsonPropertyName("cast")]
    public List<TmdbFilmeResumo> Cast { get; set; } = new();
}

public class TmdbFilmeResumo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }

    [JsonPropertyName("popularity")]
    public float Popularity { get; set; }
}

public class TmdbVideoResult
{
    [JsonPropertyName("results")]
    public List<TmdbVideo> Results { get; set; } = new();
}

public class TmdbVideo
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("site")]
    public string Site { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}