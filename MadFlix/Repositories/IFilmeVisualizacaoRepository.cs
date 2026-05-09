namespace MadFlix.Repositories;

public interface IFilmeVisualizacaoRepository
{
    Task<bool> ExisteAsync(int utilizadorId, int filmeId, CancellationToken cancellationToken = default);
    Task AddAsync(Models.FilmeVisualizacao visualizacao, CancellationToken cancellationToken = default);
}
