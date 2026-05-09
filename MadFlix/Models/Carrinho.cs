namespace MadFlix.Models;

public class Carrinho
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;

    public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
}
