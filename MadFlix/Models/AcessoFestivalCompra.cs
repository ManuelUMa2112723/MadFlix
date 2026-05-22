using MadFlix.Models.Acessos;

namespace MadFlix.Models;

public class AcessoFestivalCompra
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public int FestivalId { get; set; }
    public Festival? Festival { get; set; }
    public TipoAcesso TipoAcesso { get; set; } = TipoAcesso.PasseDiario;
    public DateTime InicioAcessoUtc { get; set; }
    public DateTime FimAcessoUtc { get; set; }
    public DateTime CompradoEmUtc { get; set; } = DateTime.UtcNow;
}
