using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IFestivalRepository
{
    Task<IReadOnlyList<Festival>> ListarAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Festival festival, CancellationToken cancellationToken = default);
}
