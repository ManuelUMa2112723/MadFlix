namespace MadFlix.Models;

public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string? Genero { get; set; }
    public string? Sinopse { get; set; }

    // Relación con Diretor
    public int? DiretorId { get; set; }
    public Diretor? Diretor { get; set; }

    // Relación N:M con Ator
    public ICollection<FilmeAtor> FilmeAtores { get; set; } = new List<FilmeAtor>();
}