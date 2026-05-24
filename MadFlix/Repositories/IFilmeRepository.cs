using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IFilmeRepository
{
    Task<Filme?> GetByIdAsync(int filmeId, CancellationToken cancellationToken = default);
    Task<Filme?> GetDetalhesByIdAsync(int filmeId, CancellationToken cancellationToken = default);
    Task<Filme?> GetByTituloAsync(string titulo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Filme>> ListarPorFestivalAsync(int festivalId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Filme>> ListarRecomendadosAsync(int quantidade, CancellationToken cancellationToken = default);
    Task AddAsync(Filme filme, CancellationToken cancellationToken = default);
    Task UpdateAsync(Filme filme, CancellationToken cancellationToken = default);
}
