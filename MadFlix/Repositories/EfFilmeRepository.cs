using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfFilmeRepository : IFilmeRepository
{
    private readonly AppDbContext _context;

    public EfFilmeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Filme?> GetByIdAsync(int filmeId, CancellationToken cancellationToken = default)
    {
        return await _context.Filmes
            .FirstOrDefaultAsync(filme => filme.Id == filmeId, cancellationToken);
    }

    public async Task<Filme?> GetByTituloAsync(string titulo, CancellationToken cancellationToken = default)
    {
        return await _context.Filmes
            .FirstOrDefaultAsync(
                filme => filme.Titulo.ToLower() == titulo.ToLower(),
                cancellationToken);
    }

    public async Task AddAsync(Filme filme, CancellationToken cancellationToken = default)
    {
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
