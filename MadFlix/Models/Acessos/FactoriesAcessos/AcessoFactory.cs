using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.AbstractAcessos;
using MadFlix.Models.Acessos.DecoratorsAcessos;
using MadFlix.Models.Acessos.InterfaceAcessos;

namespace MadFlix.Models.Acessos.FactoriesAcessos;

public abstract class AcessoFactory : IAcessoFactory
{
    public Acesso Criar(AcessoPedido pedido)
    {
        var acesso = CriarAcessoBase(pedido);

        foreach (var decorator in pedido.Decorators)
            acesso = CriarDecorator(decorator, acesso, pedido);

        return acesso;
    }

    protected abstract Acesso CriarAcessoBase(AcessoPedido pedido);

    protected abstract Acesso CriarDecorator(
        TipoAcessoDecorator decorator,
        Acesso acesso,
        AcessoPedido pedido);
}
