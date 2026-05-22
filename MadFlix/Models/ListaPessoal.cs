namespace MadFlix.Models;

public class ListaPessoal
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; } = null!;

    public ICollection<ListaPessoalFilme> ListaPessoalFilmes { get; set; } = new List<ListaPessoalFilme>();
}