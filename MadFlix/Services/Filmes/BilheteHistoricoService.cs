using MadFlix.Repositories;

namespace MadFlix.Services.Filmes;

public sealed class BilheteHistoricoService : IBilheteHistoricoService
{
    private readonly IFilmeCompraRepository _compraRepository;

    public BilheteHistoricoService(IFilmeCompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }

    public async Task<BilheteHistorico> ObterHistoricoAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var agoraUtc = DateTime.UtcNow;
        var compras = await _compraRepository.ListarPorUtilizadorAsync(utilizadorId, cancellationToken);

        return new BilheteHistorico
        {
            Ativos = compras
                .Where(compra => compra.InicioAcessoUtc <= agoraUtc && compra.FimAcessoUtc >= agoraUtc)
                .ToList(),
            Pendentes = compras
                .Where(compra => compra.InicioAcessoUtc > agoraUtc)
                .ToList(),
            Expirados = compras
                .Where(compra => compra.FimAcessoUtc < agoraUtc)
                .ToList()
        };
    }
}
