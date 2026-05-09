using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;
using MadFlix.Services.Notifications;

namespace MadFlix.Services.Filmes;

public sealed class FilmePurchaseService : IFilmePurchaseService
{
    private readonly IFilmeCompraRepository _compraRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly IUtilizadorRepository _utilizadorRepository;

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

        if (await _compraRepository.ExisteAsync(utilizadorId, filmeId, cancellationToken))
        {
            return ServiceResult<string>.Success($"Ja comprou '{filme.Titulo}'.");
        }

        await _compraRepository.AddAsync(new FilmeCompra
        {
            UtilizadorId = utilizadorId,
            FilmeId = filmeId,
            CompradoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Compra confirmada",
            Mensagem = $"Comprou '{filme.Titulo}' com sucesso.",
            Tipo = "compra"
        }, cancellationToken);

        return ServiceResult<string>.Success($"Compra de '{filme.Titulo}' concluida.");
    }
}
