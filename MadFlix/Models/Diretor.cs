namespace MadFlix.Models;

public class Diretor
{
    public int Id { get; set; }
    public int? TmdbId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? DataNascimento { get; set; }

    public ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}