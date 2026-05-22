using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IAcessoFestivalCompraRepository
{
    Task<IReadOnlyList<AcessoFestivalCompra>> ListarPorUtilizadorAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default);

    Task<AcessoFestivalCompra?> GetByIdAsync(
        int acessoId,
        CancellationToken cancellationToken = default);

    Task<bool> TemAcessoValidoAsync(
        int utilizadorId,
        int festivalId,
        DateTime momentoUtc,
        CancellationToken cancellationToken = default);

    Task AddAsync(AcessoFestivalCompra acesso, CancellationToken cancellationToken = default);

    Task RemoveAsync(AcessoFestivalCompra acesso, CancellationToken cancellationToken = default);
}
