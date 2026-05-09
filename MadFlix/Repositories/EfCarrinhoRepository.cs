using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfCarrinhoRepository : ICarrinhoRepository
{
    private readonly AppDbContext _context;

    public EfCarrinhoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Carrinho?> GetByUtilizadorIdAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Carrinhos
            .Include(carrinho => carrinho.Itens)
            .ThenInclude(item => item.Festival)
            .Include(carrinho => carrinho.Itens)
            .ThenInclude(item => item.Filme)
            .FirstOrDefaultAsync(carrinho => carrinho.UtilizadorId == utilizadorId, cancellationToken);
    }

    public async Task<CarrinhoItem?> GetItemByIdAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        return await _context.CarrinhoItens
            .Include(item => item.Carrinho)
            .FirstOrDefaultAsync(item => item.Id == itemId, cancellationToken);
    }

    public async Task AddAsync(Carrinho carrinho, CancellationToken cancellationToken = default)
    {
        _context.Carrinhos.Add(carrinho);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddItemAsync(CarrinhoItem item, CancellationToken cancellationToken = default)
    {
        _context.CarrinhoItens.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveItemAsync(CarrinhoItem item, CancellationToken cancellationToken = default)
    {
        _context.CarrinhoItens.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
