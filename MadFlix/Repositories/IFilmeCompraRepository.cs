namespace MadFlix.Repositories;

public interface IFilmeCompraRepository
{
    Task<bool> ExisteAsync(int utilizadorId, int filmeId, CancellationToken cancellationToken = default);
    Task AddAsync(Models.FilmeCompra compra, CancellationToken cancellationToken = default);
}
