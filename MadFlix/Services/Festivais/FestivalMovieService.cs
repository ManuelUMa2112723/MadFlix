using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Models.Festivais.ObserverFestivais;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalMovieService : IFestivalMovieService
{
    private readonly FilmeService _filmeService;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IFestivalRepository _festivalRepository;
    private readonly IFestivalSubject _festivalSubject;

    public FestivalMovieService(
        FilmeService filmeService,
        IFilmeRepository filmeRepository,
        IFestivalRepository festivalRepository,
        IFestivalSubject festivalSubject,
        IFestivalObserver festivalObserver)
    {
        _filmeService = filmeService;
        _filmeRepository = filmeRepository;
        _festivalRepository = festivalRepository;
        _festivalSubject = festivalSubject;
        _festivalSubject.Attach(festivalObserver);
    }

    public async Task<IReadOnlyList<Filme>> ListarFilmesAsync(
        int festivalId,
        CancellationToken cancellationToken = default)
    {
        return await _filmeRepository.ListarPorFestivalAsync(festivalId, cancellationToken);
    }

    public async Task<ServiceResult<string>> AdicionarFilmeAsync(
        int festivalId,
        string titulo,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return ServiceResult<string>.Failure("Introduza o titulo do filme.");

        var festival = await _festivalRepository.GetByIdAsync(festivalId, cancellationToken);

        if (festival is null)
            return ServiceResult<string>.Failure("Festival nao encontrado.");

        var filme = await _filmeService.BuscarFilmeAsync(titulo.Trim());

        if (filme is null)
            return ServiceResult<string>.Failure($"Filme '{titulo.Trim()}' nao encontrado.");

        if (filme.FestivalId == festival.Id)
            return ServiceResult<string>.Success($"'{filme.Titulo}' ja pertence ao festival {festival.Nome}.");

        filme.FestivalId = festival.Id;
        await _filmeRepository.UpdateAsync(filme, cancellationToken);

        await _festivalSubject.Notify(new FestivalFilmeAdicionadoEvent
        {
            FestivalId = festival.Id,
            FestivalNome = festival.Nome,
            FilmeId = filme.Id,
            FilmeTitulo = filme.Titulo
        }, cancellationToken);

        return ServiceResult<string>.Success($"'{filme.Titulo}' adicionado ao festival {festival.Nome}.");
    }
}
