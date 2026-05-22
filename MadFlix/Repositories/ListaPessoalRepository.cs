using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class ListaPessoalRepository : IListaPessoalRepository
{
    private readonly AppDbContext _context;

    public ListaPessoalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ListaPessoal>> GetByUtilizadorIdAsync(int utilizadorId, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPessoais
            .Include(l => l.ListaPessoalFilmes)
                .ThenInclude(lf => lf.Filme)
            .Where(l => l.UtilizadorId == utilizadorId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ListaPessoal?> GetByIdAsync(int listaId, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPessoais
            .Include(l => l.ListaPessoalFilmes)
                .ThenInclude(lf => lf.Filme)
            .FirstOrDefaultAsync(l => l.Id == listaId, cancellationToken);
    }

    public async Task AddAsync(ListaPessoal lista, CancellationToken cancellationToken = default)
    {
        _context.ListasPessoais.Add(lista);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ListaPessoal lista, CancellationToken cancellationToken = default)
    {
        _context.ListasPessoais.Remove(lista);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddFilmeAsync(ListaPessoalFilme listaPessoalFilme, CancellationToken cancellationToken = default)
    {
        _context.ListasPessoaisFilmes.Add(listaPessoalFilme);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveFilmeAsync(ListaPessoalFilme listaPessoalFilme, CancellationToken cancellationToken = default)
    {
        _context.ListasPessoaisFilmes.Remove(listaPessoalFilme);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ListaPessoalFilme?> GetFilmeAsync(int listaId, int filmeId, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPessoaisFilmes
            .FirstOrDefaultAsync(lf => lf.ListaPessoalId == listaId && lf.FilmeId == filmeId, cancellationToken);
    }
}