using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Filmes;

public sealed class FilmeRecommendationService : IFilmeRecommendationService
{
    private const int QuantidadePadrao = 6;
    private readonly IFilmeRepository _filmeRepository;

    public FilmeRecommendationService(IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }

    public async Task<IReadOnlyList<Filme>> ListarParaPaginaPrincipalAsync(
        int quantidade,
        CancellationToken cancellationToken = default)
    {
        var limite = quantidade > 0 ? quantidade : QuantidadePadrao;
        return await _filmeRepository.ListarRecomendadosAsync(limite, cancellationToken);
    }
}
