using MadFlix.Extensions;
using MadFlix.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadFlix.Services.Notifications;

public sealed class UserNotificationCenter : IUserNotificationFeed, IObserver<UserNotification>, IDisposable
{
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly List<UserNotification> _notifications = new();
    private IDisposable? _subscription;
    private int? _currentUserId;

    public UserNotificationCenter(
        IUserNotificationSubject notificationSubject,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _notificationSubject = notificationSubject;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public event Action? Changed;

    public IReadOnlyList<UserNotification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Count > 0;

    public async Task RefreshSubscriptionAsync()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var nextUserId = authenticationState.User.GetMadFlixUserId();

        if (_currentUserId == nextUserId)
        {
            return;
        }

        _subscription?.Dispose();
        _subscription = null;
        _currentUserId = nextUserId;
        _notifications.Clear();

        if (nextUserId.HasValue)
        {
            _subscription = _notificationSubject.Subscribe(nextUserId.Value, this);
        }

        Changed?.Invoke();
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(UserNotification value)
    {
        _notifications.Insert(0, value);

        if (_notifications.Count > 5)
        {
            _notifications.RemoveAt(_notifications.Count - 1);
        }

        Changed?.Invoke();
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}
