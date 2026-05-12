namespace MadFlix.Models.Festivais.ObserverFestivais;

public sealed class FestivalSubject : IFestivalSubject
{
    private readonly List<IFestivalObserver> _observers = new();
    private readonly object _lock = new();

    public void Attach(IFestivalObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        lock (_lock)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }
    }

    public void Detach(IFestivalObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        lock (_lock)
        {
            _observers.Remove(observer);
        }
    }

    public async Task Notify(
        FestivalCriadoEvent evento,
        CancellationToken cancellationToken = default)
    {
        List<IFestivalObserver> observers;

        lock (_lock)
        {
            observers = _observers.ToList();
        }

        foreach (var observer in observers)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await observer.Update(evento, cancellationToken);
        }
    }

    public async Task Notify(
        FestivalFilmeAdicionadoEvent evento,
        CancellationToken cancellationToken = default)
    {
        List<IFestivalObserver> observers;

        lock (_lock)
        {
            observers = _observers.ToList();
        }

        foreach (var observer in observers)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await observer.Update(evento, cancellationToken);
        }
    }
}
