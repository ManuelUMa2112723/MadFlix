using MadFlix.Models;

namespace MadFlix.Services.Notifications;

public interface IUserNotificationSubject
{
    IDisposable Subscribe(int utilizadorId, IObserver<UserNotification> observer);
    Task NotifyUserAsync(
        int utilizadorId,
        UserNotification notificacao,
        CancellationToken cancellationToken = default);

    Task NotifyAllAsync(
        UserNotification notificacao,
        CancellationToken cancellationToken = default);
}
