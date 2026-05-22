namespace MadFlix.Repositories;

public interface IFilmeCompraRepository
{
    Task<bool> ExisteAsync(int utilizadorId, int filmeId, CancellationToken cancellationToken = default);
    Task<bool> TemAcessoValidoAsync(
        int utilizadorId,
        int filmeId,
        DateTime momentoUtc,
        CancellationToken cancellationToken = default);

    Task AddAsync(Models.FilmeCompra compra, CancellationToken cancellationToken = default);
}
