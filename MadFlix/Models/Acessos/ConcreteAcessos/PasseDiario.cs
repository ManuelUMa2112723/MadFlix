using MadFlix.Models.Acessos.AbstractAcessos;

namespace MadFlix.Models.Acessos.ConcreteAcessos;

public sealed class PasseDiario : Acesso
{
    public PasseDiario(
        int festivalId,
        DateTime diaFestival,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm) : base(preco, compradoEm)
    {
        if (festivalId <= 0)
            throw new ArgumentException("O festival do passe diario e obrigatorio.", nameof(festivalId));

        if (fimFestival.Date < inicioFestival.Date)
            throw new ArgumentException("A data de fim do festival nao pode ser anterior a data de inicio.", nameof(fimFestival));

        var dia = diaFestival.Date;

        if (dia < inicioFestival.Date || dia > fimFestival.Date)
            throw new ArgumentException("O dia escolhido tem de pertencer ao periodo do festival.", nameof(diaFestival));

        FestivalId = festivalId;
        DiaFestival = dia;
        InicioFestival = inicioFestival.Date;
        FimFestival = fimFestival.Date;
    }

    public int FestivalId { get; }
    public DateTime DiaFestival { get; }
    public DateTime InicioFestival { get; }
    public DateTime FimFestival { get; }
    public override TipoAcesso Tipo => TipoAcesso.PasseDiario;
    public override string Descricao => "Passe valido para todas as sessoes do festival no dia escolhido.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(DiaFestival, DiaFestival.AddDays(1));
    }
}
