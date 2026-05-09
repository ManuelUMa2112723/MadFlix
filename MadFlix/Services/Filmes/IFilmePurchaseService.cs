using MadFlix.Contracts.Common;

namespace MadFlix.Services.Filmes;

public interface IFilmePurchaseService
{
    Task<ServiceResult<string>> ComprarAsync(
        int utilizadorId,
        int filmeId,
        CancellationToken cancellationToken = default);
}
