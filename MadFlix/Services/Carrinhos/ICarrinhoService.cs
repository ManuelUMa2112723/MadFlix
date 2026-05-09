using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Models.Acessos;

namespace MadFlix.Services.Carrinhos;

public interface ICarrinhoService
{
    Task<IReadOnlyList<CarrinhoItem>> ListarItensAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> AdicionarAcessoFestivalAsync(
        int utilizadorId,
        int festivalId,
        TipoAcesso tipoAcesso,
        DateTime? diaFestival,
        int? horasAluguer,
        decimal preco,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> RemoverItemAsync(
        int utilizadorId,
        int itemId,
        CancellationToken cancellationToken = default);
}
