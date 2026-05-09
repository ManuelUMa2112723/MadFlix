namespace MadFlix.Models.Acessos.AbstractAcessos;

public abstract class Acesso
{
    protected Acesso(decimal preco, DateTime compradoEm)
    {
        if (preco < 0)
            throw new ArgumentException("O preco do acesso nao pode ser negativo.", nameof(preco));

        Preco = preco;
        CompradoEm = compradoEm;
    }

    public decimal Preco { get; }
    public DateTime CompradoEm { get; }
    public abstract TipoAcesso Tipo { get; }
    public abstract string Descricao { get; }

    public abstract JanelaAcesso ObterJanelaAcesso();

    public bool EstaAtivo(DateTime momento)
    {
        return ObterJanelaAcesso().Contem(momento);
    }
}
