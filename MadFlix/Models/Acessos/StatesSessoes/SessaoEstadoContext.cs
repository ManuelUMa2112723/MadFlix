namespace MadFlix.Models.Acessos.StatesSessoes;

public sealed class SessaoEstadoContext
{
    public SessaoEstadoContext(DateTime inicio, DateTime fim, DateTime momento)
    {
        Inicio = inicio;
        Fim = fim;
        State = momento >= inicio && momento < fim
            ? new SessaoDisponivelState()
            : new SessaoIndisponivelState();
    }

    public DateTime Inicio { get; }
    public DateTime Fim { get; }
    public ISessaoEstado State { get; }
    public string Nome => State.Nome;
    public string ClasseCss => State.ClasseCss;
    public bool Disponivel => State.Disponivel;
}
