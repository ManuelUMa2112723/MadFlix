using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Filmes;

public interface IFilmeCatalogService
{
    Task<ServiceResult<Filme>> BuscarFilmeAsync(string titulo, CancellationToken cancellationToken = default);
}
