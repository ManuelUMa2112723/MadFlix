using MadFlix.Models;

namespace MadFlix.Factories;

public class FestivalFactory : IFestivalFactory
{
    public Festival CriarFestivalDeGenero(
        string nome,
        string genero,
        DateTime dataInicio,
        DateTime dataFim,
        string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do festival e obrigatorio.", nameof(nome));

        if (string.IsNullOrWhiteSpace(genero))
            throw new ArgumentException("O genero do festival e obrigatorio.", nameof(genero));

        if (dataFim < dataInicio)
            throw new ArgumentException("A data de fim nao pode ser anterior a data de inicio.", nameof(dataFim));

        return new Festival
        {
            Nome = nome.Trim(),
            Genero = genero.Trim(),
            DataInicio = dataInicio,
            DataFim = dataFim,
            Descricao = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim()
        };
    }
}
