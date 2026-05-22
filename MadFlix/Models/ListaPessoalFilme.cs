namespace MadFlix.Models;

public class ListaPessoalFilme
{
    public int ListaPessoalId { get; set; }
    public ListaPessoal ListaPessoal { get; set; } = null!;

    public int FilmeId { get; set; }
    public Filme Filme { get; set; } = null!;
}