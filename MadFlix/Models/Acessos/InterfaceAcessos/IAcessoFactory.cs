using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.AbstractAcessos;
using MadFlix.Models.Acessos.FactoriesAcessos;

namespace MadFlix.Models.Acessos.InterfaceAcessos;

public interface IAcessoFactory
{
    Acesso Criar(AcessoPedido pedido);
}
