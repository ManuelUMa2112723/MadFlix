using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalCreationService
{
    Task<Festival> CriarAsync(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao,
        CancellationToken cancellationToken = default);
}
