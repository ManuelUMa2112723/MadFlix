using MadFlix.Models;
using MadFlix.Models.Festivais.InterfaceFestivais;
using MadFlix.Models.Festivais.ObserverFestivais;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalCreationService : IFestivalCreationService
{
    private readonly IFestivalFactory _festivalFactory;
    private readonly IFestivalRepository _festivalRepository;
    private readonly IFestivalSubject _festivalSubject;

    public FestivalCreationService(
        IFestivalFactory festivalFactory,
        IFestivalRepository festivalRepository,
        IFestivalSubject festivalSubject,
        IFestivalObserver festivalObserver)
    {
        _festivalFactory = festivalFactory;
        _festivalRepository = festivalRepository;
        _festivalSubject = festivalSubject;
        _festivalSubject.Attach(festivalObserver);
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

        await _festivalSubject.Notify(new FestivalCriadoEvent
        {
            FestivalId = festival.Id,
            FestivalNome = festival.Nome,
            FestivalGenero = festival.Genero,
            DataInicio = festival.DataInicio,
            DataFim = festival.DataFim
        }, cancellationToken);

        return festival;
    }
}
