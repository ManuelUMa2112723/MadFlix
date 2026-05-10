namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public sealed class PagamentoCartaoBancarioStrategy : IPagamentoStrategy
{
    public string Nome => "Cartao bancario";
    public string Descricao => "Pagamento atraves de cartao bancario.";

    public string ProcessarPagamento(decimal total)
    {
        return $"Processando pagamento por cartao bancario no valor de {total:C}.";
    }
}
