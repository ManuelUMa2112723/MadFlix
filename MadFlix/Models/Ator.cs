namespace MadFlix.Models;

public class Ator
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? DataNascimento { get; set; }

    public ICollection<FilmeAtor> FilmeAtores { get; set; } = new List<FilmeAtor>();
}