using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.DecoratorsAcessos;

namespace MadFlix.Models.Acessos.FactoriesAcessos;

public sealed class AcessoPedido
{
    public TipoAcesso Tipo { get; set; }
    public int? FilmeId { get; set; }
    public int? SessaoId { get; set; }
    public int? FestivalId { get; set; }
    public DateTime? DiaFestival { get; set; }
    public DateTime? InicioSessao { get; set; }
    public DateTime? FimSessao { get; set; }
    public DateTime? InicioFestival { get; set; }
    public DateTime? FimFestival { get; set; }
    public int? HorasAluguer { get; set; }
    public decimal Preco { get; set; }
    public DateTime CompradoEm { get; set; }
    public List<TipoAcessoDecorator> Decorators { get; } = new();
}
