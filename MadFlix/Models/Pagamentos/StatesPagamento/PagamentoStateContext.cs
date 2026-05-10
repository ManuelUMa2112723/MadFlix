namespace MadFlix.Models.Pagamentos.StatesPagamento;

public sealed class PagamentoStateContext
{
    public PagamentoStateContext()
    {
        State = new ProcessandoPagamentoState();
    }

    public IPagamentoState State { get; private set; }
    public string NomeEstado => State.Nome;
    public string MensagemEstado => State.Mensagem;
    public bool Concluido => State is CompraConcluidaState;

    public void SetState(IPagamentoState state)
    {
        State = state;
    }

    public async Task Request(CancellationToken cancellationToken = default)
    {
        await State.Handle(this, cancellationToken);
    }
}
