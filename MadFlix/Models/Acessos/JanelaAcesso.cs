namespace MadFlix.Models.Acessos;

public sealed record JanelaAcesso(DateTime Inicio, DateTime Fim)
{
    public bool Contem(DateTime momento)
    {
        return momento >= Inicio && momento < Fim;
    }
}
