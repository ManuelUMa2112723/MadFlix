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
    private const string ApiKey = "61fda4c685f2c495c309f0610bc82974";
    private const string BaseUrl = "https://api.themoviedb.org/3";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

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

        // 2 — Buscar en TMDB por título
        var searchUrl = $"{BaseUrl}/search/movie?query={Uri.EscapeDataString(titulo)}&api_key={ApiKey}&language=en-US";
        var searchResposta = await _httpClient.GetStringAsync(searchUrl);
        var searchResult = JsonSerializer.Deserialize<TmdbSearchResult>(searchResposta, _jsonOptions);

        if (searchResult == null || searchResult.Results.Count == 0)
            return null;

        var tmdbId = searchResult.Results[0].TmdbId;

        // 3 — Obtener detalles completos
        var detailUrl = $"{BaseUrl}/movie/{tmdbId}?api_key={ApiKey}&language=en-US";
        var detailResposta = await _httpClient.GetStringAsync(detailUrl);
        var tmdbFilme = JsonSerializer.Deserialize<TmdbFilme>(detailResposta, _jsonOptions);

        if (tmdbFilme == null)
            return null;

        // 4 — Obtener créditos (director y actores)
        var creditsUrl = $"{BaseUrl}/movie/{tmdbId}/credits?api_key={ApiKey}&language=en-US";
        var creditsResposta = await _httpClient.GetStringAsync(creditsUrl);
        var credits = JsonSerializer.Deserialize<TmdbCredits>(creditsResposta, _jsonOptions);

        // 5 — Convertir con el Adapter
        var novoFilme = TmdbFilmeAdapter.ToFilme(tmdbFilme);

        // 6 — Guardar Director
        if (credits != null)
        {
            var diretorTmdb = credits.Crew.FirstOrDefault(c => c.Job == "Director");
            if (diretorTmdb != null)
            {
                var diretor = await _context.Diretores
                    .FirstOrDefaultAsync(d => d.TmdbId == diretorTmdb.Id);

                if (diretor == null)
                {
                    diretor = new Diretor
                    {
                        TmdbId = diretorTmdb.Id,
                        Nome = diretorTmdb.Nome
                    };
                    _context.Diretores.Add(diretor);
                    await _context.SaveChangesAsync();
                }

                novoFilme.DiretorId = diretor.Id;
            }

            // 7 — Guardar Actores (primeros 5)
            var atoresPrincipais = credits.Cast.Take(5).ToList();
            foreach (var atorTmdb in atoresPrincipais)
            {
                var ator = await _context.Atores
                    .FirstOrDefaultAsync(a => a.TmdbId == atorTmdb.Id);

                if (ator == null)
                {
                    ator = new Ator
                    {
                        TmdbId = atorTmdb.Id,
                        Nome = atorTmdb.Nome
                    };
                    _context.Atores.Add(ator);
                    await _context.SaveChangesAsync();
                }

                novoFilme.FilmeAtores.Add(new FilmeAtor
                {
                    AtorId = ator.Id
                });
            }
        }

        // 8 — Guardar Filme en BD
        _context.Filmes.Add(novoFilme);
        await _context.SaveChangesAsync();

        return novoFilme;
    }

    public async Task<Filme?> BuscarPorTmdbIdAsync(int tmdbId)
    {
        return await _context.Filmes
            .Include(f => f.Diretor)
            .Include(f => f.FilmeAtores)
                .ThenInclude(fa => fa.Ator)
            .FirstOrDefaultAsync(f => f.TmdbId == tmdbId);
    }

    public async Task<List<Filme>> GetRecomendadosAsync()
    {
        return await _context.Filmes
            .OrderByDescending(f => f.ImdbRating)
            .Take(8)
            .ToListAsync();
    }
    public async Task<(string Nome, List<TmdbFilmeResumo> Filmes)?> BuscarFilmesPorAtorAsync(string nomeAtor)
    {
        // 1 — Buscar actor en BD primero
        var atorBD = await _context.Atores
            .FirstOrDefaultAsync(a => a.Nome.ToLower() == nomeAtor.ToLower());

        int? tmdbAtorId = atorBD?.TmdbId;
        string? nomeEncontrado = atorBD?.Nome;

        // 2 — Si no está en BD, buscar en TMDB
        if (tmdbAtorId == null)
        {
            var searchUrl = $"{BaseUrl}/search/person?query={Uri.EscapeDataString(nomeAtor)}&api_key={ApiKey}&language=en-US";
            var searchResposta = await _httpClient.GetStringAsync(searchUrl);
            var searchResult = JsonSerializer.Deserialize<TmdbPersonSearchResult>(searchResposta, _jsonOptions);

            if (searchResult == null || searchResult.Results.Count == 0)
                return null;

            tmdbAtorId = searchResult.Results[0].Id;
            nomeEncontrado = searchResult.Results[0].Nome;
        }

        // 3 — Buscar películas del actor en TMDB
        var creditsUrl = $"{BaseUrl}/person/{tmdbAtorId}/movie_credits?api_key={ApiKey}&language=en-US";
        var creditsResposta = await _httpClient.GetStringAsync(creditsUrl);
        var credits = JsonSerializer.Deserialize<TmdbPersonCredits>(creditsResposta, _jsonOptions);

        if (credits == null)
            return null;

        // 4 — Devolver top 10 por popularidad
        var filmes = credits.Cast
            .OrderByDescending(f => f.Popularity)
            .Take(10)
            .ToList();

        return (nomeEncontrado!, filmes);
    }
}