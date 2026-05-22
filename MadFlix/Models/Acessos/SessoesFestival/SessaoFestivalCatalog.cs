namespace MadFlix.Models.Acessos.SessoesFestival;

public static class SessaoFestivalCatalog
{
    public static IReadOnlyList<SessaoFestival> SessoesPasseDiario { get; } =
    [
        new SessaoFestival("Sessao 1", new TimeSpan(11, 30, 0), new TimeSpan(16, 30, 0)),
        new SessaoFestival("Sessao 2", new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0))
    ];
}
