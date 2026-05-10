namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public sealed class PagamentoMbWayStrategy : IPagamentoStrategy
{
    public string Nome => "MB Way";
    public string Descricao => "Pagamento atraves de confirmacao MB Way.";

    public string ProcessarPagamento(decimal total)
    {
        return $"Processando pagamento MB Way no valor de {total:C}.";
    }
}
