namespace MadFlix.Models.Festivais.ObserverFestivais;

public interface IFestivalSubject
{
    void Attach(IFestivalObserver observer);
    void Detach(IFestivalObserver observer);

    Task Notify(
        FestivalCriadoEvent evento,
        CancellationToken cancellationToken = default);

    Task Notify(
        FestivalFilmeAdicionadoEvent evento,
        CancellationToken cancellationToken = default);
}
