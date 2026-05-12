using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfComentarioFilmeRepository : IComentarioFilmeRepository
{
    private readonly AppDbContext _context;

    public EfComentarioFilmeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ComentarioFilme>> ListarPorFilmeAsync(
        int filmeId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ComentariosFilmes
            .Include(comentario => comentario.Utilizador)
            .Where(comentario => comentario.FilmeId == filmeId)
            .OrderByDescending(comentario => comentario.CriadoEmUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<ComentarioFilme?> GetByIdAsync(
        int comentarioId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ComentariosFilmes
            .Include(comentario => comentario.Utilizador)
            .FirstOrDefaultAsync(comentario => comentario.Id == comentarioId, cancellationToken);
    }

    public async Task AddAsync(
        ComentarioFilme comentario,
        CancellationToken cancellationToken = default)
    {
        _context.ComentariosFilmes.Add(comentario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        ComentarioFilme comentario,
        CancellationToken cancellationToken = default)
    {
        _context.ComentariosFilmes.Remove(comentario);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
