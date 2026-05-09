using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalMovieService
{
    Task<IReadOnlyList<Filme>> ListarFilmesAsync(
        int festivalId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> AdicionarFilmeAsync(
        int festivalId,
        string titulo,
        CancellationToken cancellationToken = default);
}
