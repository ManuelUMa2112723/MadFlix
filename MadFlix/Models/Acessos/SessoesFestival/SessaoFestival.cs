namespace MadFlix.Models.Acessos.SessoesFestival;

public sealed record SessaoFestival(string Nome, TimeSpan Inicio, TimeSpan Fim)
{
    public DateTime ObterInicio(DateTime dia)
    {
        return dia.Date.Add(Inicio);
    }

    public DateTime ObterFim(DateTime dia)
    {
        return dia.Date.Add(Fim);
    }
}
