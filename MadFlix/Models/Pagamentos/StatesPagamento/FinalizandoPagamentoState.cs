namespace MadFlix.Models.Pagamentos.StatesPagamento;

public sealed class FinalizandoPagamentoState : IPagamentoState
{
    public string Nome => "Finalizando pagamento";
    public string Mensagem => "Finalizando pagamento...";

    public async Task Handle(PagamentoStateContext context, CancellationToken cancellationToken = default)
    {
        await Task.Delay(800, cancellationToken);
        context.SetState(new CompraConcluidaState());
    }
}
