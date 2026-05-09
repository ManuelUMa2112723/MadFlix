using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfFilmeCompraRepository : IFilmeCompraRepository
{
    private readonly AppDbContext _context;

    public EfFilmeCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsync(int utilizadorId, int filmeId, CancellationToken cancellationToken = default)
    {
        return await _context.FilmeCompras
            .AnyAsync(
                compra => compra.UtilizadorId == utilizadorId && compra.FilmeId == filmeId,
                cancellationToken);
    }

    public async Task AddAsync(FilmeCompra compra, CancellationToken cancellationToken = default)
    {
        _context.FilmeCompras.Add(compra);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
