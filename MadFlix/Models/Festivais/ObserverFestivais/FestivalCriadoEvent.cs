namespace MadFlix.Models.Festivais.ObserverFestivais;

public sealed class FestivalCriadoEvent
{
    public int FestivalId { get; init; }
    public string FestivalNome { get; init; } = string.Empty;
    public string FestivalGenero { get; init; } = string.Empty;
    public DateTime DataInicio { get; init; }
    public DateTime DataFim { get; init; }
}
