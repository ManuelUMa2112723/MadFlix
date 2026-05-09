using MadFlix.Models;

namespace MadFlix.Services.Notifications;

public interface IUserNotificationFeed
{
    event Action? Changed;
    IReadOnlyList<UserNotification> Notifications { get; }
    bool HasNotifications { get; }
    Task RefreshSubscriptionAsync();
}
