using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Models.Acessos;
using MadFlix.Repositories;
using MadFlix.Services.Notifications;

namespace MadFlix.Services.Filmes;

public sealed class FilmePurchaseService : IFilmePurchaseService
{
    private readonly IFilmeCompraRepository _compraRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly IUtilizadorRepository _utilizadorRepository;
    private const int HoraSessaoIndividualTeste = 11;
    private const int MinutoSessaoIndividualTeste = 30;
    private static readonly TimeSpan DuracaoJanelaBilheteSessao = TimeSpan.FromHours(5);

    public FilmePurchaseService(
        IFilmeCompraRepository compraRepository,
        IFilmeRepository filmeRepository,
        IUserNotificationSubject notificationSubject,
        IUtilizadorRepository utilizadorRepository)
    {
        _compraRepository = compraRepository;
        _filmeRepository = filmeRepository;
        _notificationSubject = notificationSubject;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<ServiceResult<string>> ComprarAsync(
        int utilizadorId,
        int filmeId,
        CancellationToken cancellationToken = default)
    {
        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);

        if (utilizador is null)
        {
            return ServiceResult<string>.Failure("Utilizador nao encontrado.");
        }

        var filme = await _filmeRepository.GetByIdAsync(filmeId, cancellationToken);

        if (filme is null)
        {
            return ServiceResult<string>.Failure("Filme nao encontrado.");
        }

        if (await _compraRepository.TemAcessoValidoAsync(utilizadorId, filmeId, DateTime.UtcNow, cancellationToken))
        {
            return ServiceResult<string>.Success($"Ja tem um bilhete de sessao valido para '{filme.Titulo}'.");
        }

        var inicioSessao = ObterInicioProximaSessaoIndividual();
        var fimSessao = inicioSessao.Add(DuracaoJanelaBilheteSessao);

        await _compraRepository.AddAsync(new FilmeCompra
        {
            UtilizadorId = utilizadorId,
            FilmeId = filmeId,
            TipoAcesso = TipoAcesso.BilheteSessao,
            InicioAcessoUtc = inicioSessao.ToUniversalTime(),
            FimAcessoUtc = fimSessao.ToUniversalTime(),
            CompradoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Compra confirmada",
            Mensagem = $"Comprou bilhete de sessao para '{filme.Titulo}' com sucesso.",
            Tipo = "compra"
        }, cancellationToken);

        return ServiceResult<string>.Success($"Bilhete de sessao para '{filme.Titulo}' concluido.");
    }

    private static DateTime ObterInicioProximaSessaoIndividual()
    {
        var agora = DateTime.Now;
        var inicioHoje = agora.Date.AddHours(HoraSessaoIndividualTeste).AddMinutes(MinutoSessaoIndividualTeste);
        var fimHoje = inicioHoje.Add(DuracaoJanelaBilheteSessao);

        return agora > fimHoje
            ? inicioHoje.AddDays(1)
            : inicioHoje;
    }
}
