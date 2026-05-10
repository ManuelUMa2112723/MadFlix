namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public sealed class PagamentoPaypalStrategy : IPagamentoStrategy
{
    public string Nome => "PayPal";
    public string Descricao => "Pagamento atraves da conta PayPal.";

    public string ProcessarPagamento(decimal total)
    {
        return $"Processando pagamento PayPal no valor de {total:C}.";
    }
}
