using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalCatalogService : IFestivalCatalogService
{
    private readonly IFestivalRepository _festivalRepository;

    public FestivalCatalogService(IFestivalRepository festivalRepository)
    {
        _festivalRepository = festivalRepository;
    }

    public async Task<IReadOnlyList<Festival>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _festivalRepository.ListarAsync(cancellationToken);
    }
}
