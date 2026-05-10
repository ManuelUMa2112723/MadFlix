namespace MadFlix.Models.Pagamentos.StatesPagamento;

public sealed class ProcessandoPagamentoState : IPagamentoState
{
    public string Nome => "Processando pagamento";
    public string Mensagem => "Processando pagamento...";

    public async Task Handle(PagamentoStateContext context, CancellationToken cancellationToken = default)
    {
        await Task.Delay(800, cancellationToken);
        context.SetState(new FinalizandoPagamentoState());
    }
}
