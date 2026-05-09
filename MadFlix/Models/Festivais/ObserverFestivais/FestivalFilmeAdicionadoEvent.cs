namespace MadFlix.Models.Festivais.ObserverFestivais;

public sealed class FestivalFilmeAdicionadoEvent
{
    public int FestivalId { get; init; }
    public string FestivalNome { get; init; } = string.Empty;
    public int FilmeId { get; init; }
    public string FilmeTitulo { get; init; } = string.Empty;
}
