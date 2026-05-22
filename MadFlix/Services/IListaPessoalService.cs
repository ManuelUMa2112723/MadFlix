using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services;

public interface IListaPessoalService
{
    Task<IEnumerable<ListaPessoal>> ListarAsync(int utilizadorId, CancellationToken cancellationToken = default);
    Task<ServiceResult<ListaPessoal>> CriarAsync(int utilizadorId, string nome, CancellationToken cancellationToken = default);
    Task<ServiceResult> EliminarAsync(int listaId, int utilizadorId, CancellationToken cancellationToken = default);
    Task<ServiceResult> AdicionarFilmeAsync(int listaId, int filmeId, int utilizadorId, CancellationToken cancellationToken = default);
    Task<ServiceResult> RemoverFilmeAsync(int listaId, int filmeId, int utilizadorId, CancellationToken cancellationToken = default);
}