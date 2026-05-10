namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public static class PagamentoStrategyFactory
{
    public static IReadOnlyList<IPagamentoStrategy> CriarMetodos()
    {
        return
        [
            new PagamentoPaypalStrategy(),
            new PagamentoMbWayStrategy(),
            new PagamentoCartaoBancarioStrategy(),
            new PagamentoApplePayStrategy()
        ];
    }
}
