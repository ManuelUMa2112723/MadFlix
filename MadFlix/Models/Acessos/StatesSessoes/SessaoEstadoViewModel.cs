namespace MadFlix.Models.Acessos.StatesSessoes;

public sealed class SessaoEstadoViewModel
{
    public string Nome { get; init; } = string.Empty;
    public DateTime Inicio { get; init; }
    public DateTime Fim { get; init; }
    public string Estado { get; init; } = string.Empty;
    public string ClasseCss { get; init; } = string.Empty;
    public bool Disponivel { get; init; }
}
