namespace MadFlix.Models.Acessos.StatesSessoes;

public interface ISessaoEstado
{
    string Nome { get; }
    string ClasseCss { get; }
    bool Disponivel { get; }
}
