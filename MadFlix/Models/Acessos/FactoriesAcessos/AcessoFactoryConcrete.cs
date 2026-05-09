using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.AbstractAcessos;
using MadFlix.Models.Acessos.ConcreteAcessos;
using MadFlix.Models.Acessos.DecoratorsAcessos;

namespace MadFlix.Models.Acessos.FactoriesAcessos;

public class AcessoFactoryConcrete : AcessoFactory
{
    private const int HorasBaseAluguerDigital = 48;

    protected override Acesso CriarAcessoBase(AcessoPedido pedido)
    {
        return pedido.Tipo switch
        {
            TipoAcesso.BilheteSessao => new BilheteSessao(
                ObterObrigatorio(pedido.FilmeId, nameof(pedido.FilmeId)),
                ObterObrigatorio(pedido.SessaoId, nameof(pedido.SessaoId)),
                ObterObrigatorio(pedido.InicioSessao, nameof(pedido.InicioSessao)),
                ObterObrigatorio(pedido.FimSessao, nameof(pedido.FimSessao)),
                pedido.Preco,
                pedido.CompradoEm),

            TipoAcesso.PasseDiario => new PasseDiario(
                ObterObrigatorio(pedido.FestivalId, nameof(pedido.FestivalId)),
                ObterObrigatorio(pedido.DiaFestival, nameof(pedido.DiaFestival)),
                ObterObrigatorio(pedido.InicioFestival, nameof(pedido.InicioFestival)),
                ObterObrigatorio(pedido.FimFestival, nameof(pedido.FimFestival)),
                pedido.Preco,
                pedido.CompradoEm),

            TipoAcesso.PasseCompleto => new PasseCompleto(
                ObterObrigatorio(pedido.FestivalId, nameof(pedido.FestivalId)),
                ObterObrigatorio(pedido.InicioFestival, nameof(pedido.InicioFestival)),
                ObterObrigatorio(pedido.FimFestival, nameof(pedido.FimFestival)),
                pedido.Preco,
                pedido.CompradoEm),

            TipoAcesso.AluguerDigital => new AluguerDigital(
                ObterObrigatorio(pedido.FestivalId, nameof(pedido.FestivalId)),
                TimeSpan.FromHours(HorasBaseAluguerDigital),
                pedido.Preco,
                pedido.CompradoEm),

            _ => throw new InvalidOperationException("Tipo de acesso invalido.")
        };
    }

    protected override Acesso CriarDecorator(
        TipoAcessoDecorator decorator,
        Acesso acesso,
        AcessoPedido pedido)
    {
        return decorator switch
        {
            TipoAcessoDecorator.DuracaoAluguerDigital => new DuracaoAluguerDigitalDecorator(
                acesso,
                TimeSpan.FromHours(ObterHorasAluguer(pedido))),
            _ => acesso
        };
    }

    private static int ObterHorasAluguer(AcessoPedido pedido)
    {
        var horas = pedido.HorasAluguer ?? HorasBaseAluguerDigital;

        if (horas <= 0)
            throw new ArgumentException("A duracao do aluguer tem de ser positiva.", nameof(pedido.HorasAluguer));

        return horas;
    }

    private static T ObterObrigatorio<T>(T? valor, string nomeCampo) where T : struct
    {
        if (!valor.HasValue)
            throw new ArgumentException($"O campo {nomeCampo} e obrigatorio.", nomeCampo);

        return valor.Value;
    }
}
