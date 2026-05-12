using MadFlix.Extensions;
using MadFlix.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadFlix.Services.Notifications;

public sealed class UserNotificationCenter : IUserNotificationFeed, IObserver<UserNotification>, IDisposable
{
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly List<UserNotification> _notifications = new();
    private readonly Dictionary<UserNotification, CancellationTokenSource> _expirationTimers = new();
    private IDisposable? _subscription;
    private int? _currentUserId;
    private bool _disposed;

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
        LimparTemporizadores();
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
        AgendarRemocao(value);

        if (_notifications.Count > 5)
        {
            var ultima = _notifications[^1];
            _notifications.RemoveAt(_notifications.Count - 1);
            CancelarTemporizador(ultima);
        }

        Changed?.Invoke();
    }

    public void Dispose()
    {
        _disposed = true;
        _subscription?.Dispose();
        LimparTemporizadores();
    }

    private void AgendarRemocao(UserNotification notification)
    {
        var cancellation = new CancellationTokenSource();
        _expirationTimers[notification] = cancellation;

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancellation.Token);

                if (_disposed)
                    return;

                if (_notifications.Remove(notification))
                {
                    _expirationTimers.Remove(notification);
                    Changed?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                cancellation.Dispose();
            }
        });
    }

    private void CancelarTemporizador(UserNotification notification)
    {
        if (_expirationTimers.Remove(notification, out var cancellation))
            cancellation.Cancel();
    }

    private void LimparTemporizadores()
    {
        foreach (var cancellation in _expirationTimers.Values)
            cancellation.Cancel();

        _expirationTimers.Clear();
    }
}
