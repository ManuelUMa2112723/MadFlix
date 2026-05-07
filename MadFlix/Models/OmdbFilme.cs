using System.Text.Json.Serialization;

namespace MadFlix.Models;

public class OmdbFilme
{
    [JsonPropertyName("Title")]
    public string Titulo { get; set; } = string.Empty;

    [JsonPropertyName("Year")]
    public string Ano { get; set; } = string.Empty;

    [JsonPropertyName("Genre")]
    public string Genero { get; set; } = string.Empty;

    [JsonPropertyName("Plot")]
    public string Sinopse { get; set; } = string.Empty;

    [JsonPropertyName("Poster")]
    public string Poster { get; set; } = string.Empty;

    [JsonPropertyName("imdbID")]
    public string ImdbId { get; set; } = string.Empty;

    [JsonPropertyName("imdbRating")]
    public string ImdbRating { get; set; } = string.Empty;

    [JsonPropertyName("Runtime")]
    public string Runtime { get; set; } = string.Empty;

    [JsonPropertyName("Rated")]
    public string Rated { get; set; } = string.Empty;

    [JsonPropertyName("Director")]
    public string Diretor { get; set; } = string.Empty;

    [JsonPropertyName("Actors")]
    public string Atores { get; set; } = string.Empty;

    [JsonPropertyName("Response")]
    public string Response { get; set; } = string.Empty;
}