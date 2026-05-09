using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalCatalogService
{
    Task<IReadOnlyList<Festival>> ListarAsync(CancellationToken cancellationToken = default);
}
