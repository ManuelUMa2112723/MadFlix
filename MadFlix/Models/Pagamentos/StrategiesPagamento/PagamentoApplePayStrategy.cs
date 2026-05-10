namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public sealed class PagamentoApplePayStrategy : IPagamentoStrategy
{
    public string Nome => "Apple Pay";
    public string Descricao => "Pagamento atraves de Apple Pay.";

    public string ProcessarPagamento(decimal total)
    {
        return $"Processando pagamento Apple Pay no valor de {total:C}.";
    }
}
