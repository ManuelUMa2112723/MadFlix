namespace MadFlix.Models.Festivais.ObserverFestivais;

public interface IFestivalObserver
{
    Task Update(
        FestivalCriadoEvent evento,
        CancellationToken cancellationToken = default);

    Task Update(
        FestivalFilmeAdicionadoEvent evento,
        CancellationToken cancellationToken = default);
}
