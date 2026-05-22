namespace MadFlix.Models;

public class Utilizador
{
    public int Id { get; set; }
    public string NomeUtilizador { get; set; } = string.Empty;
    public string NomeUtilizadorNormalizado { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string EmailNormalizado { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;
    public bool IsAdmin { get; set; } = false;

    public ICollection<FilmeCompra> Compras { get; set; } = new List<FilmeCompra>();
    public ICollection<AcessoFestivalCompra> AcessosFestival { get; set; } = new List<AcessoFestivalCompra>();
    public ICollection<FilmeVisualizacao> Visualizacoes { get; set; } = new List<FilmeVisualizacao>();
    public ICollection<ComentarioFilme> ComentariosFilmes { get; set; } = new List<ComentarioFilme>();
    public Carrinho? Carrinho { get; set; }
}
