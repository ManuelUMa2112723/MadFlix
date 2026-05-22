namespace MadFlix.Models.Acessos.StatesSessoes;

public sealed class SessaoDisponivelState : ISessaoEstado
{
    public string Nome => "Disponivel";
    public string ClasseCss => "session-state available";
    public bool Disponivel => true;
}
