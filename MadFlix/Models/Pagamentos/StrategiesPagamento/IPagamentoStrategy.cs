namespace MadFlix.Models.Pagamentos.StrategiesPagamento;

public interface IPagamentoStrategy
{
    string Nome { get; }
    string Descricao { get; }
    string ProcessarPagamento(decimal total);
}
