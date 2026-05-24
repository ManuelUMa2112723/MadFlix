using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfFestivalFilmeVotoRepository : IFestivalFilmeVotoRepository
{
    private readonly AppDbContext _context;

    public EfFestivalFilmeVotoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FestivalFilmeVoto?> GetByFestivalAndUtilizadorAsync(
        int festivalId,
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.FestivalFilmeVotos
            .FirstOrDefaultAsync(
                voto => voto.FestivalId == festivalId && voto.UtilizadorId == utilizadorId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<FestivalFilmeVotoResumo>> ListarResumoPorFestivalAsync(
        int festivalId,
        CancellationToken cancellationToken = default)
    {
        return await _context.FestivalFilmeVotos
            .Where(voto => voto.FestivalId == festivalId)
            .GroupBy(voto => voto.FilmeId)
            .Select(grupo => new FestivalFilmeVotoResumo
            {
                FilmeId = grupo.Key,
                TotalVotos = grupo.Count()
            })
            .OrderByDescending(resumo => resumo.TotalVotos)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FestivalFilmeVoto voto, CancellationToken cancellationToken = default)
    {
        _context.FestivalFilmeVotos.Add(voto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FestivalFilmeVoto voto, CancellationToken cancellationToken = default)
    {
        _context.FestivalFilmeVotos.Update(voto);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
