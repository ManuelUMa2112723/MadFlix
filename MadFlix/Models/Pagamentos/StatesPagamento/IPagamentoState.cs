namespace MadFlix.Models.Pagamentos.StatesPagamento;

public interface IPagamentoState
{
    string Nome { get; }
    string Mensagem { get; }
    Task Handle(PagamentoStateContext context, CancellationToken cancellationToken = default);
}
