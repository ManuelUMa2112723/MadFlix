using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Repositories;

public sealed class EfFestivalRepository : IFestivalRepository
{
    private readonly AppDbContext _context;

    public EfFestivalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Festival>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Festivais
            .OrderBy(festival => festival.DataInicio)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Festival festival, CancellationToken cancellationToken = default)
    {
        _context.Festivais.Add(festival);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
