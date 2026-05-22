using MadFlix.Contracts.Common;

namespace MadFlix.Services.Filmes;

public interface IFilmePlaybackService
{
    Task<ServiceResult<string>> PodeAssistirAsync(
        int utilizadorId,
        int filmeId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> AssistirAsync(
        int utilizadorId,
        int filmeId,
        CancellationToken cancellationToken = default);
}
