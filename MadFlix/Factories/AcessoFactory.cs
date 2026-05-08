using MadFlix.Models.Acessos;

namespace MadFlix.Factories;

public class AcessoFactory : IAcessoFactory
{
    public BilheteSessao CriarBilheteSessao(
        int filmeId,
        int sessaoId,
        DateTime inicioSessao,
        DateTime fimSessao,
        decimal preco,
        DateTime compradoEm)
    {
        return new BilheteSessao(
            filmeId,
            sessaoId,
            inicioSessao,
            fimSessao,
            preco,
            compradoEm);
    }

    public PasseDiario CriarPasseDiario(
        int festivalId,
        DateTime diaFestival,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm)
    {
        return new PasseDiario(
            festivalId,
            diaFestival,
            inicioFestival,
            fimFestival,
            preco,
            compradoEm);
    }

    public PasseCompleto CriarPasseCompleto(
        int festivalId,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm)
    {
        return new PasseCompleto(
            festivalId,
            inicioFestival,
            fimFestival,
            preco,
            compradoEm);
    }

    public AluguerDigital CriarAluguerDigital(
        int festivalId,
        TimeSpan duracao,
        decimal preco,
        DateTime compradoEm)
    {
        return new AluguerDigital(
            festivalId,
            duracao,
            preco,
            compradoEm);
    }
}
