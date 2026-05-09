namespace MadFlix.Models;

public class Festival
{
    public Festival()
    {
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }

    public ICollection<Filme> Filmes { get; set; } = new List<Filme>();
    public ICollection<CarrinhoItem> CarrinhoItens { get; set; } = new List<CarrinhoItem>();
}
