using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfUtilizadorRepository : IUtilizadorRepository
{
    private readonly AppDbContext _context;

    public EfUtilizadorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Utilizador?> GetByIdAsync(int utilizadorId, CancellationToken cancellationToken = default)
    {
        return await _context.Utilizadores
            .FirstOrDefaultAsync(utilizador => utilizador.Id == utilizadorId, cancellationToken);
    }

    public async Task<Utilizador?> GetByIdentidadeNormalizadaAsync(
        string identidadeNormalizada,
        CancellationToken cancellationToken = default)
    {
        return await _context.Utilizadores
            .FirstOrDefaultAsync(
                utilizador =>
                    utilizador.NomeUtilizadorNormalizado == identidadeNormalizada ||
                    utilizador.EmailNormalizado == identidadeNormalizada,
                cancellationToken);
    }

    public async Task<bool> ExisteNomeUtilizadorAsync(
        string nomeUtilizadorNormalizado,
        CancellationToken cancellationToken = default)
    {
        return await _context.Utilizadores
            .AnyAsync(
                utilizador => utilizador.NomeUtilizadorNormalizado == nomeUtilizadorNormalizado,
                cancellationToken);
    }

    public async Task<bool> ExisteEmailAsync(string emailNormalizado, CancellationToken cancellationToken = default)
    {
        return await _context.Utilizadores
            .AnyAsync(utilizador => utilizador.EmailNormalizado == emailNormalizado, cancellationToken);
    }

    public async Task AddAsync(Utilizador utilizador, CancellationToken cancellationToken = default)
    {
        _context.Utilizadores.Add(utilizador);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Utilizador utilizador, CancellationToken cancellationToken = default)
    {
        _context.Utilizadores.Update(utilizador);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
