using MadFlix.Models;

namespace MadFlix.Services.Filmes;

public interface IFilmeRecommendationService
{
    Task<IReadOnlyList<Filme>> ListarParaPaginaPrincipalAsync(
        int quantidade,
        CancellationToken cancellationToken = default);
}
