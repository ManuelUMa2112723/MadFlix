using MadFlix.Models;

namespace MadFlix.Repositories;

public interface IUtilizadorRepository
{
    Task<Utilizador?> GetByIdAsync(int utilizadorId, CancellationToken cancellationToken = default);
    Task<Utilizador?> GetByIdentidadeNormalizadaAsync(string identidadeNormalizada, CancellationToken cancellationToken = default);
    Task<bool> ExisteNomeUtilizadorAsync(string nomeUtilizadorNormalizado, CancellationToken cancellationToken = default);
    Task<bool> ExisteEmailAsync(string emailNormalizado, CancellationToken cancellationToken = default);
    Task AddAsync(Utilizador utilizador, CancellationToken cancellationToken = default);
    Task UpdateAsync(Utilizador utilizador, CancellationToken cancellationToken = default);
}
