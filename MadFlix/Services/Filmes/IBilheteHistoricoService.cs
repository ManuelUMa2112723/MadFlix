using MadFlix.Models;

namespace MadFlix.Services.Filmes;

public interface IBilheteHistoricoService
{
    Task<BilheteHistorico> ObterHistoricoAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default);
}

public sealed class BilheteHistorico
{
    public IReadOnlyList<FilmeCompra> Ativos { get; init; } = [];
    public IReadOnlyList<FilmeCompra> Pendentes { get; init; } = [];
    public IReadOnlyList<FilmeCompra> Expirados { get; init; } = [];
}
