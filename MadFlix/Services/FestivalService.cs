using MadFlix.Data;
using MadFlix.Factories;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Services;

public class FestivalService
{
    private readonly AppDbContext _context;
    private readonly IFestivalFactory _festivalFactory;

    public FestivalService(AppDbContext context, IFestivalFactory festivalFactory)
    {
        _context = context;
        _festivalFactory = festivalFactory;
    }

    public async Task<List<Festival>> ListarAsync()
    {
        return await _context.Festivais
            .OrderBy(f => f.DataInicio)
            .ToListAsync();
    }

    public async Task<Festival> CriarAsync(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao)
    {
        var festival = _festivalFactory.CriarFestivalDeGenero(
            nome,
            genero,
            dataInicio,
            dataFim,
            descricao);

        _context.Festivais.Add(festival);
        await _context.SaveChangesAsync();

        return festival;
    }
}
