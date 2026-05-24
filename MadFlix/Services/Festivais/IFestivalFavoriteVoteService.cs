using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalFavoriteVoteService
{
    Task<ServiceResult<string>> VotarAsync(
        int festivalId,
        int filmeId,
        int utilizadorId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FestivalFilmeVotoResumo>> ListarResumoAsync(
        int festivalId,
        CancellationToken cancellationToken = default);

    Task<int?> ObterFilmeVotadoAsync(
        int festivalId,
        int utilizadorId,
        CancellationToken cancellationToken = default);
}
