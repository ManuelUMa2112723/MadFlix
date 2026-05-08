using MadFlix.Models.Acessos;

namespace MadFlix.Factories;

public interface IAcessoFactory
{
    Acesso Criar(AcessoPedido pedido);
}
