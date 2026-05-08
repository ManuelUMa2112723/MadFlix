using MadFlix.Models.Acessos;

namespace MadFlix.Factories;

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
