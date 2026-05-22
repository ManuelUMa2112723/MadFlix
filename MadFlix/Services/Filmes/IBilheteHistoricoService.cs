using MadFlix.Models;
using MadFlix.Contracts.Common;
using MadFlix.Models.Acessos;

namespace MadFlix.Services.Filmes;

public interface IBilheteHistoricoService
{
    Task<BilheteHistorico> ObterHistoricoAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<string>> RemoverBilheteAsync(
        int utilizadorId,
        int bilheteId,
        bool acessoFestival,
        CancellationToken cancellationToken = default);
}

public sealed class BilheteHistorico
{
    public IReadOnlyList<HistoricoAcessoItem> Ativos { get; init; } = [];
    public IReadOnlyList<HistoricoAcessoItem> Pendentes { get; init; } = [];
    public IReadOnlyList<HistoricoAcessoItem> Expirados { get; init; } = [];
}

public sealed class HistoricoAcessoItem
{
    public int Id { get; init; }
    public int? FilmeId { get; init; }
    public int? FestivalId { get; init; }
    public bool AcessoFestival { get; init; }
    public string Nome { get; init; } = string.Empty;
    public TipoAcesso TipoAcesso { get; init; }
    public DateTime InicioAcessoUtc { get; init; }
    public DateTime FimAcessoUtc { get; init; }
    public DateTime CompradoEmUtc { get; init; }
}
