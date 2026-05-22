using MadFlix.Models;


namespace MadFlix.Repositories;

public interface IListaPessoalRepository
{
    Task<IEnumerable<ListaPessoal>> GetByUtilizadorIdAsync(int utilizadorId, CancellationToken cancellationToken = default);
    Task<ListaPessoal?> GetByIdAsync(int listaId, CancellationToken cancellationToken = default);
    Task AddAsync(ListaPessoal lista, CancellationToken cancellationToken = default);
    Task DeleteAsync(ListaPessoal lista, CancellationToken cancellationToken = default);
    Task AddFilmeAsync(ListaPessoalFilme listaPessoalFilme, CancellationToken cancellationToken = default);
    Task RemoveFilmeAsync(ListaPessoalFilme listaPessoalFilme, CancellationToken cancellationToken = default);
    Task<ListaPessoalFilme?> GetFilmeAsync(int listaId, int filmeId, CancellationToken cancellationToken = default);
}