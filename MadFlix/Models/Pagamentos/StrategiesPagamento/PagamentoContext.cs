namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public sealed class PagamentoContext
{
    private IPagamentoStrategy? _strategy;

    public void SetStrategy(IPagamentoStrategy strategy)
    {
        _strategy = strategy;
    }

    public string ExecutarPagamento(decimal total)
    {
        if (_strategy is null)
            throw new InvalidOperationException("Escolha um metodo de pagamento.");

        return _strategy.ProcessarPagamento(total);
    }
}
