using MadFlix.Models.Acessos;

namespace MadFlix.Factories;

public interface IAcessoFactory
{
    BilheteSessao CriarBilheteSessao(
        int filmeId,
        int sessaoId,
        DateTime inicioSessao,
        DateTime fimSessao,
        decimal preco,
        DateTime compradoEm);

    PasseDiario CriarPasseDiario(
        int festivalId,
        DateTime diaFestival,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm);

    PasseCompleto CriarPasseCompleto(
        int festivalId,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm);

    AluguerDigital CriarAluguerDigital(
        int filmeId,
        TimeSpan duracao,
        decimal preco,
        DateTime compradoEm);
}
