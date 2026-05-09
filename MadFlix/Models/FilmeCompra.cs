namespace MadFlix.Models;

public class FilmeCompra
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public DateTime CompradoEmUtc { get; set; } = DateTime.UtcNow;
}
