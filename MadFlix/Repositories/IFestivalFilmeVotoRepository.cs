using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IFestivalFilmeVotoRepository
{
    Task<FestivalFilmeVoto?> GetByFestivalAndUtilizadorAsync(
        int festivalId,
        int utilizadorId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FestivalFilmeVotoResumo>> ListarResumoPorFestivalAsync(
        int festivalId,
        CancellationToken cancellationToken = default);

    Task AddAsync(FestivalFilmeVoto voto, CancellationToken cancellationToken = default);
    Task UpdateAsync(FestivalFilmeVoto voto, CancellationToken cancellationToken = default);
}
