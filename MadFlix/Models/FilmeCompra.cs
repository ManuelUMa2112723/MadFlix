using MadFlix.Models.Acessos;

namespace MadFlix.Models;

public class FilmeCompra
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public TipoAcesso TipoAcesso { get; set; } = TipoAcesso.BilheteSessao;
    public DateTime InicioAcessoUtc { get; set; }
    public DateTime FimAcessoUtc { get; set; }
    public DateTime CompradoEmUtc { get; set; } = DateTime.UtcNow;
}
