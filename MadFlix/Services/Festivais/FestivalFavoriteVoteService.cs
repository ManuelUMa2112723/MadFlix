using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Festivais;

public sealed class FestivalFavoriteVoteService : IFestivalFavoriteVoteService
{
    private readonly IFestivalRepository _festivalRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IFestivalFilmeVotoRepository _votoRepository;

    public FestivalFavoriteVoteService(
        IFestivalRepository festivalRepository,
        IFilmeRepository filmeRepository,
        IFestivalFilmeVotoRepository votoRepository)
    {
        _festivalRepository = festivalRepository;
        _filmeRepository = filmeRepository;
        _votoRepository = votoRepository;
    }

    public async Task<ServiceResult<string>> VotarAsync(
        int festivalId,
        int filmeId,
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var festival = await _festivalRepository.GetByIdAsync(festivalId, cancellationToken);

        if (festival is null)
            return ServiceResult<string>.Failure("Festival nao encontrado.");

        var filme = await _filmeRepository.GetByIdAsync(filmeId, cancellationToken);

        if (filme is null || filme.FestivalId != festival.Id)
            return ServiceResult<string>.Failure("Escolha um filme que pertenca ao catalogo deste festival.");

        var voto = await _votoRepository.GetByFestivalAndUtilizadorAsync(
            festivalId,
            utilizadorId,
            cancellationToken);

        if (voto is null)
        {
            voto = new FestivalFilmeVoto
            {
                FestivalId = festivalId,
                FilmeId = filmeId,
                UtilizadorId = utilizadorId
            };

            await _votoRepository.AddAsync(voto, cancellationToken);
        }
        else
        {
            voto.FilmeId = filmeId;
            voto.AtualizadoEmUtc = DateTime.UtcNow;
            await _votoRepository.UpdateAsync(voto, cancellationToken);
        }

        return ServiceResult<string>.Success($"Voto registado em '{filme.Titulo}' para {festival.Nome}.");
    }

    public async Task<IReadOnlyList<FestivalFilmeVotoResumo>> ListarResumoAsync(
        int festivalId,
        CancellationToken cancellationToken = default)
    {
        return await _votoRepository.ListarResumoPorFestivalAsync(festivalId, cancellationToken);
    }

    public async Task<int?> ObterFilmeVotadoAsync(
        int festivalId,
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var voto = await _votoRepository.GetByFestivalAndUtilizadorAsync(
            festivalId,
            utilizadorId,
            cancellationToken);

        return voto?.FilmeId;
    }
}
