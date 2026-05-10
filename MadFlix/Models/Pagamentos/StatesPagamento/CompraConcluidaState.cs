namespace MadFlix.Models.Pagamentos.StatesPagamento;

public sealed class CompraConcluidaState : IPagamentoState
{
    public string Nome => "Compra concluida";
    public string Mensagem => "Compra concluida com sucesso.";

    public Task Handle(PagamentoStateContext context, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
