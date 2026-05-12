using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IComentarioFilmeRepository
{
    Task<IReadOnlyList<ComentarioFilme>> ListarPorFilmeAsync(
        int filmeId,
        CancellationToken cancellationToken = default);

    Task<ComentarioFilme?> GetByIdAsync(
        int comentarioId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ComentarioFilme comentario,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        ComentarioFilme comentario,
        CancellationToken cancellationToken = default);
}
