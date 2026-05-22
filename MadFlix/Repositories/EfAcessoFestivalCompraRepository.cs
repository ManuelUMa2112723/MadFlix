using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfAcessoFestivalCompraRepository : IAcessoFestivalCompraRepository
{
    private readonly AppDbContext _context;

    public EfAcessoFestivalCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AcessoFestivalCompra>> ListarPorUtilizadorAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AcessosFestivalCompras
            .Include(acesso => acesso.Festival)
            .Where(acesso => acesso.UtilizadorId == utilizadorId)
            .OrderByDescending(acesso => acesso.CompradoEmUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<AcessoFestivalCompra?> GetByIdAsync(
        int acessoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AcessosFestivalCompras
            .Include(acesso => acesso.Festival)
            .FirstOrDefaultAsync(acesso => acesso.Id == acessoId, cancellationToken);
    }

    public async Task<bool> TemAcessoValidoAsync(
        int utilizadorId,
        int festivalId,
        DateTime momentoUtc,
        CancellationToken cancellationToken = default)
    {
        return await _context.AcessosFestivalCompras
            .AnyAsync(
                acesso =>
                    acesso.UtilizadorId == utilizadorId &&
                    acesso.FestivalId == festivalId &&
                    acesso.InicioAcessoUtc <= momentoUtc &&
                    acesso.FimAcessoUtc > momentoUtc,
                cancellationToken);
    }

    public async Task AddAsync(AcessoFestivalCompra acesso, CancellationToken cancellationToken = default)
    {
        _context.AcessosFestivalCompras.Add(acesso);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(AcessoFestivalCompra acesso, CancellationToken cancellationToken = default)
    {
        _context.AcessosFestivalCompras.Remove(acesso);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
