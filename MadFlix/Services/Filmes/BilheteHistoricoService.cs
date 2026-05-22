using MadFlix.Repositories;
using MadFlix.Contracts.Common;

namespace MadFlix.Services.Filmes;

public sealed class BilheteHistoricoService : IBilheteHistoricoService
{
    private readonly IAcessoFestivalCompraRepository _acessoFestivalCompraRepository;
    private readonly IFilmeCompraRepository _compraRepository;

    public BilheteHistoricoService(
        IAcessoFestivalCompraRepository acessoFestivalCompraRepository,
        IFilmeCompraRepository compraRepository)
    {
        _acessoFestivalCompraRepository = acessoFestivalCompraRepository;
        _compraRepository = compraRepository;
    }

    public async Task<BilheteHistorico> ObterHistoricoAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var agoraUtc = DateTime.UtcNow;
        var comprasFilmes = await _compraRepository.ListarPorUtilizadorAsync(utilizadorId, cancellationToken);
        var comprasFestivais = await _acessoFestivalCompraRepository.ListarPorUtilizadorAsync(utilizadorId, cancellationToken);
        var acessos = comprasFilmes
            .Select(compra => new HistoricoAcessoItem
            {
                Id = compra.Id,
                FilmeId = compra.FilmeId,
                Nome = compra.Filme?.Titulo ?? "Filme",
                TipoAcesso = compra.TipoAcesso,
                InicioAcessoUtc = compra.InicioAcessoUtc,
                FimAcessoUtc = compra.FimAcessoUtc,
                CompradoEmUtc = compra.CompradoEmUtc
            })
            .Concat(comprasFestivais.Select(acesso => new HistoricoAcessoItem
            {
                Id = acesso.Id,
                FestivalId = acesso.FestivalId,
                AcessoFestival = true,
                Nome = acesso.Festival?.Nome ?? "Festival",
                TipoAcesso = acesso.TipoAcesso,
                InicioAcessoUtc = acesso.InicioAcessoUtc,
                FimAcessoUtc = acesso.FimAcessoUtc,
                CompradoEmUtc = acesso.CompradoEmUtc
            }))
            .OrderByDescending(acesso => acesso.CompradoEmUtc)
            .ToList();

        return new BilheteHistorico
        {
            Ativos = acessos
                .Where(compra => compra.InicioAcessoUtc <= agoraUtc && compra.FimAcessoUtc > agoraUtc)
                .ToList(),
            Pendentes = acessos
                .Where(compra => compra.InicioAcessoUtc > agoraUtc)
                .ToList(),
            Expirados = acessos
                .Where(compra => compra.FimAcessoUtc <= agoraUtc)
                .ToList()
        };
    }

    public async Task<ServiceResult<string>> RemoverBilheteAsync(
        int utilizadorId,
        int bilheteId,
        bool acessoFestival,
        CancellationToken cancellationToken = default)
    {
        if (acessoFestival)
        {
            var acesso = await _acessoFestivalCompraRepository.GetByIdAsync(bilheteId, cancellationToken);

            if (acesso is null || acesso.UtilizadorId != utilizadorId)
                return ServiceResult<string>.Failure("Acesso nao encontrado.");

            await _acessoFestivalCompraRepository.RemoveAsync(acesso, cancellationToken);

            return ServiceResult<string>.Success("Acesso removido e cancelado.");
        }

        var bilhete = await _compraRepository.GetByIdAsync(bilheteId, cancellationToken);

        if (bilhete is null || bilhete.UtilizadorId != utilizadorId)
            return ServiceResult<string>.Failure("Bilhete nao encontrado.");

        await _compraRepository.RemoveAsync(bilhete, cancellationToken);

        return ServiceResult<string>.Success("Bilhete removido e acesso cancelado.");
    }
}
