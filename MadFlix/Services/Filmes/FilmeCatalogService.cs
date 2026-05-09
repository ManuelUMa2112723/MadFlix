using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Services;

namespace MadFlix.Services.Filmes;

public sealed class FilmeCatalogService : IFilmeCatalogService
{
    private readonly FilmeService _filmeService;

    public FilmeCatalogService(FilmeService filmeService)
    {
        _filmeService = filmeService;
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
}
