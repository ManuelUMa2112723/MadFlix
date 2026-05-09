using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.AbstractAcessos;

namespace MadFlix.Models.Acessos.DecoratorsAcessos;

public abstract class AcessoDecorator : Acesso
{
    protected AcessoDecorator(Acesso acesso) : base(acesso.Preco, acesso.CompradoEm)
    {
        Acesso = acesso;
    }

    protected Acesso Acesso { get; }

    public override TipoAcesso Tipo => Acesso.Tipo;
    public override string Descricao => Acesso.Descricao;
    public override JanelaAcesso ObterJanelaAcesso()
    {
        return Acesso.ObterJanelaAcesso();
    }
}
