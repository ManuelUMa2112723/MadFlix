namespace MadFlix.Models;

public class FestivalFilmeVoto
{
    public int Id { get; set; }
    public int FestivalId { get; set; }
    public int FilmeId { get; set; }
    public int UtilizadorId { get; set; }
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEmUtc { get; set; } = DateTime.UtcNow;

    public Festival? Festival { get; set; }
    public Filme? Filme { get; set; }
    public Utilizador? Utilizador { get; set; }
}
