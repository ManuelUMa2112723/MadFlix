namespace MadFlix.Models;

public class FilmeVisualizacao
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public DateTime VisualizadoEmUtc { get; set; } = DateTime.UtcNow;
}
