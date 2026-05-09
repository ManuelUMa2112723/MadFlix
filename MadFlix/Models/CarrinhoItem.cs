using MadFlix.Models.Acessos;

namespace MadFlix.Models;

public class CarrinhoItem
{
    public int Id { get; set; }
    public int CarrinhoId { get; set; }
    public Carrinho? Carrinho { get; set; }
    public TipoAcesso TipoAcesso { get; set; }
    public decimal Preco { get; set; }
    public DateTime InicioAcessoUtc { get; set; }
    public DateTime FimAcessoUtc { get; set; }
    public DateTime AdicionadoEmUtc { get; set; } = DateTime.UtcNow;

    public int? FestivalId { get; set; }
    public Festival? Festival { get; set; }
    public int? FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public int? SessaoId { get; set; }
    public DateTime? DiaFestival { get; set; }
    public int? HorasAluguer { get; set; }
}
