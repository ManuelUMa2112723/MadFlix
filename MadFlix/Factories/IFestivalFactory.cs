using MadFlix.Models;

namespace MadFlix.Factories;

public interface IFestivalFactory
{
    Festival CriarFestivalDeGenero(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao = null);
}
