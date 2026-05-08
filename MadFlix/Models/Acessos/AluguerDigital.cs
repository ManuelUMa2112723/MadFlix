namespace MadFlix.Models.Acessos;

public sealed class AluguerDigital : Acesso
{
    public AluguerDigital(
        int festivalId,
        TimeSpan duracao,
        decimal preco,
        DateTime compradoEm) : base(preco, compradoEm)
    {
        if (festivalId <= 0)
            throw new ArgumentException("O festival do aluguer e obrigatorio.", nameof(festivalId));

        if (duracao <= TimeSpan.Zero)
            throw new ArgumentException("A duracao do aluguer tem de ser positiva.", nameof(duracao));

        FestivalId = festivalId;
        Duracao = duracao;
    }

    public int FestivalId { get; }
    public TimeSpan Duracao { get; }
    public override TipoAcesso Tipo => TipoAcesso.AluguerDigital;
    public override string Descricao => "Aluguer digital do festival valido durante uma janela temporal.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(CompradoEm, CompradoEm.Add(Duracao));
    }
}
