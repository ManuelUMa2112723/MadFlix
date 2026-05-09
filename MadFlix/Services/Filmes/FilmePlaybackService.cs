using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;
using MadFlix.Services.Notifications;

namespace MadFlix.Services.Filmes;

public sealed class FilmePlaybackService : IFilmePlaybackService
{
    private readonly IFilmeRepository _filmeRepository;
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly IUtilizadorRepository _utilizadorRepository;
    private readonly IFilmeVisualizacaoRepository _visualizacaoRepository;

    public FilmePlaybackService(
        IFilmeRepository filmeRepository,
        IUserNotificationSubject notificationSubject,
        IUtilizadorRepository utilizadorRepository,
        IFilmeVisualizacaoRepository visualizacaoRepository)
    {
        _filmeRepository = filmeRepository;
        _notificationSubject = notificationSubject;
        _utilizadorRepository = utilizadorRepository;
        _visualizacaoRepository = visualizacaoRepository;
    }

    public async Task<ServiceResult<string>> AssistirAsync(
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

        if (await _visualizacaoRepository.ExisteAsync(utilizadorId, filmeId, cancellationToken))
        {
            return ServiceResult<string>.Success($"'{filme.Titulo}' ja esta disponivel na sua area.");
        }

        await _visualizacaoRepository.AddAsync(new FilmeVisualizacao
        {
            UtilizadorId = utilizadorId,
            FilmeId = filmeId,
            VisualizadoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Sessao iniciada",
            Mensagem = $"Ja pode assistir a '{filme.Titulo}'.",
            Tipo = "visualizacao"
        }, cancellationToken);

        return ServiceResult<string>.Success($"A reproducao de '{filme.Titulo}' ficou disponivel.");
    }
}
