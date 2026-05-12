using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Filmes;

public interface IComentarioFilmeService
{
    Task<IReadOnlyList<ComentarioFilme>> ListarPorFilmeAsync(
        int filmeId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> AdicionarAsync(
        int filmeId,
        int utilizadorId,
        int estrelas,
        string texto,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> RemoverAsync(
        int comentarioId,
        CancellationToken cancellationToken = default);
}
