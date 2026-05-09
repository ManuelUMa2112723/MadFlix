using MadFlix.Models;

namespace MadFlix.Repositories;

public interface ICarrinhoRepository
{
    Task<Carrinho?> GetByUtilizadorIdAsync(int utilizadorId, CancellationToken cancellationToken = default);
    Task<CarrinhoItem?> GetItemByIdAsync(int itemId, CancellationToken cancellationToken = default);
    Task AddAsync(Carrinho carrinho, CancellationToken cancellationToken = default);
    Task AddItemAsync(CarrinhoItem item, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(CarrinhoItem item, CancellationToken cancellationToken = default);
}
