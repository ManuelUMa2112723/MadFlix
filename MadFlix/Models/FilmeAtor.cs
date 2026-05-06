namespace MadFlix.Models;

public class FilmeAtor
{
    public int FilmeId { get; set; }
    public Filme Filme { get; set; } = null!;

    public int AtorId { get; set; }
    public Ator Ator { get; set; } = null!;

    public string? Papel { get; set; }
}