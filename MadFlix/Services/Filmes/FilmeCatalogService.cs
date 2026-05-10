using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;
using MadFlix.Services;

namespace MadFlix.Services.Filmes;

public sealed class FilmeCatalogService : IFilmeCatalogService
{
    private readonly FilmeService _filmeService;
    private readonly IFilmeRepository _filmeRepository;

    public FilmeCatalogService(FilmeService filmeService, IFilmeRepository filmeRepository)
    {
        _filmeService = filmeService;
        _filmeRepository = filmeRepository;
    }

    public async Task<ServiceResult<Filme>> BuscarFilmeAsync(
        string titulo,
        CancellationToken cancellationToken = default)
    {
        var tituloLimpo = titulo.Trim();

        if (string.IsNullOrWhiteSpace(tituloLimpo))
        {
            return ServiceResult<Filme>.Failure("Introduza um titulo para pesquisar.");
        }

        var filme = await _filmeService.BuscarFilmeAsync(tituloLimpo);

        if (filme is null)
        {
            return ServiceResult<Filme>.Failure($"Filme '{tituloLimpo}' nao encontrado.");
        }

        return ServiceResult<Filme>.Success(filme);
    }

    public async Task<ServiceResult<Filme>> ObterDetalhesAsync(
        int filmeId,
        CancellationToken cancellationToken = default)
    {
        var filme = await _filmeRepository.GetDetalhesByIdAsync(filmeId, cancellationToken);

        if (filme is null)
            return ServiceResult<Filme>.Failure("Filme nao encontrado.");

        return ServiceResult<Filme>.Success(filme);
    }
}
