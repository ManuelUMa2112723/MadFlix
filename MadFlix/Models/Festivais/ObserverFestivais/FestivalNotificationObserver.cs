using MadFlix.Models;
using MadFlix.Services.Notifications;

namespace MadFlix.Models.Festivais.ObserverFestivais;

public sealed class FestivalNotificationObserver : IFestivalObserver
{
    private readonly IUserNotificationSubject _notificationSubject;

    public FestivalNotificationObserver(IUserNotificationSubject notificationSubject)
    {
        _notificationSubject = notificationSubject;
    }

    public async Task Update(
        FestivalFilmeAdicionadoEvent evento,
        CancellationToken cancellationToken = default)
    {
        await _notificationSubject.NotifyAllAsync(new UserNotification
        {
            Titulo = "Novo filme no festival",
            Mensagem = $"'{evento.FilmeTitulo}' foi adicionado ao festival {evento.FestivalNome}.",
            Tipo = "festival"
        }, cancellationToken);
    }
}
