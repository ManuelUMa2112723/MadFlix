namespace MadFlix.Models;

public class Filme
{
    public int? TmdbId { get; set; }
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string? Genero { get; set; }
    public string? Sinopse { get; set; }

    public string? Poster { get; set; }
    public string? ImdbId { get; set; }
    public float? ImdbRating { get; set; }
    public string? Runtime { get; set; }
    public string? Rated { get; set; }

    public int? DiretorId { get; set; }
    public Diretor? Diretor { get; set; }

    public int? FestivalId { get; set; }
    public Festival? Festival { get; set; }

    public ICollection<FilmeAtor> FilmeAtores { get; set; } = new List<FilmeAtor>();
    public ICollection<FilmeCompra> Compras { get; set; } = new List<FilmeCompra>();
    public ICollection<FilmeVisualizacao> Visualizacoes { get; set; } = new List<FilmeVisualizacao>();
}
