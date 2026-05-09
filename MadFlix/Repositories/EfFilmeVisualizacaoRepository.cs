using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfFilmeVisualizacaoRepository : IFilmeVisualizacaoRepository
{
    private readonly AppDbContext _context;

    public EfFilmeVisualizacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExisteAsync(int utilizadorId, int filmeId, CancellationToken cancellationToken = default)
    {
        return await _context.FilmeVisualizacoes
            .AnyAsync(
                visualizacao => visualizacao.UtilizadorId == utilizadorId && visualizacao.FilmeId == filmeId,
                cancellationToken);
    }

    public async Task AddAsync(FilmeVisualizacao visualizacao, CancellationToken cancellationToken = default)
    {
        _context.FilmeVisualizacoes.Add(visualizacao);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
