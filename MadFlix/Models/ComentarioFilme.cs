namespace MadFlix.Models;

public class ComentarioFilme
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public int Estrelas { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;
}
