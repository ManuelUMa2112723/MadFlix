namespace MadFlix.Models.Acessos;

public sealed class PasseCompleto : Acesso
{
    public PasseCompleto(
        int festivalId,
        DateTime inicioFestival,
        DateTime fimFestival,
        decimal preco,
        DateTime compradoEm) : base(preco, compradoEm)
    {
        if (festivalId <= 0)
            throw new ArgumentException("O festival do passe completo e obrigatorio.", nameof(festivalId));

        if (fimFestival.Date < inicioFestival.Date)
            throw new ArgumentException("A data de fim do festival nao pode ser anterior a data de inicio.", nameof(fimFestival));

        FestivalId = festivalId;
        InicioFestival = inicioFestival.Date;
        FimFestival = fimFestival.Date;
    }

    public int FestivalId { get; }
    public DateTime InicioFestival { get; }
    public DateTime FimFestival { get; }
    public override TipoAcesso Tipo => TipoAcesso.PasseCompleto;
    public override string Descricao => "Passe valido para todas as sessoes do festival.";

    public override JanelaAcesso ObterJanelaAcesso()
    {
        return new JanelaAcesso(InicioFestival, FimFestival.AddDays(1));
    }
}
