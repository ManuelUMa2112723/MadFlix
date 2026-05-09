namespace MadFlix.Models.Festivais.ObserverFestivais;

public interface IFestivalObserver
{
    Task Update(
        FestivalFilmeAdicionadoEvent evento,
        CancellationToken cancellationToken = default);
}
