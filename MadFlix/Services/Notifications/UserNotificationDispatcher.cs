using System.Collections.Concurrent;
using MadFlix.Models;

namespace MadFlix.Services.Notifications;

public sealed class UserNotificationDispatcher : IUserNotificationSubject
{
    private readonly ConcurrentDictionary<int, ConcurrentDictionary<Guid, IObserver<UserNotification>>> _subscriptions = new();

    public IDisposable Subscribe(int utilizadorId, IObserver<UserNotification> observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        var observers = _subscriptions.GetOrAdd(utilizadorId, static _ => new());
        var subscriptionId = Guid.NewGuid();

        observers[subscriptionId] = observer;

        return new Subscription(_subscriptions, utilizadorId, subscriptionId);
    }

    public Task NotifyUserAsync(
        int utilizadorId,
        UserNotification notificacao,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notificacao);

        if (!_subscriptions.TryGetValue(utilizadorId, out var observers))
        {
            return Task.CompletedTask;
        }

        foreach (var observer in observers.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            observer.OnNext(notificacao);
        }

        return Task.CompletedTask;
    }

    private sealed class Subscription : IDisposable
    {
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<Guid, IObserver<UserNotification>>> _subscriptions;
        private readonly int _utilizadorId;
        private readonly Guid _subscriptionId;
        private bool _disposed;

        public Subscription(
            ConcurrentDictionary<int, ConcurrentDictionary<Guid, IObserver<UserNotification>>> subscriptions,
            int utilizadorId,
            Guid subscriptionId)
        {
            _subscriptions = subscriptions;
            _utilizadorId = utilizadorId;
            _subscriptionId = subscriptionId;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_subscriptions.TryGetValue(_utilizadorId, out var observers))
            {
                observers.TryRemove(_subscriptionId, out _);

                if (observers.IsEmpty)
                {
                    _subscriptions.TryRemove(_utilizadorId, out _);
                }
            }

            _disposed = true;
        }
    }
}
