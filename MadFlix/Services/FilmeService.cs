using System.Text.Json;
using MadFlix.Adapters;
using MadFlix.Data;
using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Services;

public class FilmeService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;
    private const string ApiKey = "ba8db339";
    private const string BaseUrl = "https://www.omdbapi.com/";

    public FilmeService(AppDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task<Filme?> BuscarFilmeAsync(string titulo)
    {
        // 1 — Buscar en la BD primero
        var filmeExistente = await _context.Filmes
            .FirstOrDefaultAsync(f => f.Titulo.ToLower() == titulo.ToLower());

        if (filmeExistente != null)
            return filmeExistente;

        // 2 — Si no existe, llamar a OMDb
        var url = $"{BaseUrl}?t={Uri.EscapeDataString(titulo)}&apikey={ApiKey}";
        var resposta = await _httpClient.GetStringAsync(url);

        var omdbFilme = JsonSerializer.Deserialize<OmdbFilme>(resposta);

        if (omdbFilme == null || omdbFilme.Response == "False")
            return null;

        // 3 — Convertir con el Adapter y guardar en BD
        var novoFilme = OmdbFilmeAdapter.ToFilme(omdbFilme);
        _context.Filmes.Add(novoFilme);
        await _context.SaveChangesAsync();

        return novoFilme;
    }
}