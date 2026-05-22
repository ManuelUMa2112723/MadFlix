namespace MadFlix.Models.Acessos.StatesSessoes;

public sealed class SessaoIndisponivelState : ISessaoEstado
{
    public string Nome => "Indisponivel";
    public string ClasseCss => "session-state unavailable";
    public bool Disponivel => false;
}
