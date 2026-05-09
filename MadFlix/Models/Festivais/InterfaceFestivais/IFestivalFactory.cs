using MadFlix.Models;

namespace MadFlix.Models.Festivais.InterfaceFestivais;

public interface IFestivalFactory
{
    Festival CriarFestivalDeGenero(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao = null);
}
