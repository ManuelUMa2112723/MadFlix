using MadFlix.Models.Acessos;

namespace MadFlix.Decorators;

public sealed class DuracaoAluguerDigitalDecorator : AcessoDecorator
{
    public DuracaoAluguerDigitalDecorator(Acesso acesso, TimeSpan novaDuracao) : base(acesso)
    {
        if (acesso.Tipo != TipoAcesso.AluguerDigital)
            throw new ArgumentException("Este decorator so pode ser aplicado a alugueres digitais.", nameof(acesso));

        if (novaDuracao <= TimeSpan.Zero)
            throw new ArgumentException("A duracao do aluguer tem de ser positiva.", nameof(novaDuracao));

        NovaDuracao = novaDuracao;
    }

    public TimeSpan NovaDuracao { get; }
    public override string Descricao => $"Aluguer digital do festival valido durante {NovaDuracao.TotalHours:0}h.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(CompradoEm, CompradoEm.Add(NovaDuracao));
    }
}
