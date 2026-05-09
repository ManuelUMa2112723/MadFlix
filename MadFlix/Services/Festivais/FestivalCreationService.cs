using MadFlix.Factories;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalCreationService : IFestivalCreationService
{
    private readonly IFestivalFactory _festivalFactory;
    private readonly IFestivalRepository _festivalRepository;

    public FestivalCreationService(IFestivalFactory festivalFactory, IFestivalRepository festivalRepository)
    {
        _festivalFactory = festivalFactory;
        _festivalRepository = festivalRepository;
    }

    public async Task<Festival> CriarAsync(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao,
        CancellationToken cancellationToken = default)
    {
        var festival = _festivalFactory.CriarFestivalDeGenero(
            nome,
            genero,
            dataInicio,
            dataFim,
            descricao);

        await _festivalRepository.AddAsync(festival, cancellationToken);

        return festival;
    }
}
