namespace MadFlix.Models.Acessos;

public sealed class BilheteSessao : Acesso
{
    public BilheteSessao(
        int filmeId,
        int sessaoId,
        DateTime inicioSessao,
        DateTime fimSessao,
        decimal preco,
        DateTime compradoEm) : base(preco, compradoEm)
    {
        if (filmeId <= 0)
            throw new ArgumentException("O filme do bilhete e obrigatorio.", nameof(filmeId));

        if (sessaoId <= 0)
            throw new ArgumentException("A sessao do bilhete e obrigatoria.", nameof(sessaoId));

        if (fimSessao <= inicioSessao)
            throw new ArgumentException("A sessao tem de terminar depois de comecar.", nameof(fimSessao));

        FilmeId = filmeId;
        SessaoId = sessaoId;
        InicioSessao = inicioSessao;
        FimSessao = fimSessao;
    }

    public int FilmeId { get; }
    public int SessaoId { get; }
    public DateTime InicioSessao { get; }
    public DateTime FimSessao { get; }
    public override TipoAcesso Tipo => TipoAcesso.BilheteSessao;
    public override string Descricao => "Bilhete valido apenas para uma sessao especifica.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(InicioSessao, FimSessao);
    }
}
