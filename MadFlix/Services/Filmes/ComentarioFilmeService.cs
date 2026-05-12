using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Filmes;

public sealed class ComentarioFilmeService : IComentarioFilmeService
{
    private readonly IComentarioFilmeRepository _comentarioRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public ComentarioFilmeService(
        IComentarioFilmeRepository comentarioRepository,
        IFilmeRepository filmeRepository,
        IUtilizadorRepository utilizadorRepository)
    {
        _comentarioRepository = comentarioRepository;
        _filmeRepository = filmeRepository;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<IReadOnlyList<ComentarioFilme>> ListarPorFilmeAsync(
        int filmeId,
        CancellationToken cancellationToken = default)
    {
        return await _comentarioRepository.ListarPorFilmeAsync(filmeId, cancellationToken);
    }

    public async Task<ServiceResult<string>> AdicionarAsync(
        int filmeId,
        int utilizadorId,
        int estrelas,
        string texto,
        CancellationToken cancellationToken = default)
    {
        if (estrelas < 1 || estrelas > 5)
            return ServiceResult<string>.Failure("Escolha uma avaliacao entre 1 e 5 estrelas.");

        var textoLimpo = texto.Trim();

        if (string.IsNullOrWhiteSpace(textoLimpo))
            return ServiceResult<string>.Failure("Escreva um comentario.");

        if (textoLimpo.Length > 600)
            return ServiceResult<string>.Failure("O comentario nao pode ter mais de 600 caracteres.");

        var filme = await _filmeRepository.GetByIdAsync(filmeId, cancellationToken);

        if (filme is null)
            return ServiceResult<string>.Failure("Filme nao encontrado.");

        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);

        if (utilizador is null)
            return ServiceResult<string>.Failure("Utilizador nao encontrado.");

        await _comentarioRepository.AddAsync(new ComentarioFilme
        {
            FilmeId = filmeId,
            UtilizadorId = utilizadorId,
            Estrelas = estrelas,
            Texto = textoLimpo,
            CriadoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        return ServiceResult<string>.Success("Comentario adicionado com sucesso.");
    }

    public async Task<ServiceResult<string>> RemoverAsync(
        int comentarioId,
        CancellationToken cancellationToken = default)
    {
        var comentario = await _comentarioRepository.GetByIdAsync(comentarioId, cancellationToken);

        if (comentario is null)
            return ServiceResult<string>.Failure("Comentario nao encontrado.");

        await _comentarioRepository.RemoveAsync(comentario, cancellationToken);
        return ServiceResult<string>.Success("Comentario removido.");
    }
}
