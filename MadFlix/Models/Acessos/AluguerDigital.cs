namespace MadFlix.Models.Acessos;

public sealed class AluguerDigital : Acesso
{
    public AluguerDigital(
        int filmeId,
        TimeSpan duracao,
        decimal preco,
        DateTime compradoEm) : base(preco, compradoEm)
    {
        if (filmeId <= 0)
            throw new ArgumentException("O filme do aluguer e obrigatorio.", nameof(filmeId));

        if (duracao <= TimeSpan.Zero)
            throw new ArgumentException("A duracao do aluguer tem de ser positiva.", nameof(duracao));

        FilmeId = filmeId;
        Duracao = duracao;
    }

    public int FilmeId { get; }
    public TimeSpan Duracao { get; }
    public override TipoAcesso Tipo => TipoAcesso.AluguerDigital;
    public override string Descricao => "Aluguer digital valido durante uma janela temporal.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(CompradoEm, CompradoEm.Add(Duracao));
    }
}
