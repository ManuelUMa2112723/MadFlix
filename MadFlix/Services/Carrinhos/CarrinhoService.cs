using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Models.Acessos;
using MadFlix.Models.Acessos.DecoratorsAcessos;
using MadFlix.Models.Acessos.FactoriesAcessos;
using MadFlix.Models.Acessos.InterfaceAcessos;
using MadFlix.Models.Acessos.SessoesFestival;
using MadFlix.Repositories;
using MadFlix.Services.Notifications;

namespace MadFlix.Services.Carrinhos;

public sealed class CarrinhoService : ICarrinhoService
{
    private readonly IAcessoFactory _acessoFactory;
    private readonly IAcessoFestivalCompraRepository _acessoFestivalCompraRepository;
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IFilmeCompraRepository _filmeCompraRepository;
    private readonly IFestivalRepository _festivalRepository;
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly IUtilizadorRepository _utilizadorRepository;
    private const int SessaoEstaticaId = 1;
    private const int HoraSessaoIndividualTeste = 11;
    private const int MinutoSessaoIndividualTeste = 30;
    private static readonly TimeSpan DuracaoJanelaBilheteSessao = TimeSpan.FromHours(5);

    public CarrinhoService(
        IAcessoFactory acessoFactory,
        IAcessoFestivalCompraRepository acessoFestivalCompraRepository,
        ICarrinhoRepository carrinhoRepository,
        IFilmeRepository filmeRepository,
        IFilmeCompraRepository filmeCompraRepository,
        IFestivalRepository festivalRepository,
        IUserNotificationSubject notificationSubject,
        IUtilizadorRepository utilizadorRepository)
    {
        _acessoFactory = acessoFactory;
        _acessoFestivalCompraRepository = acessoFestivalCompraRepository;
        _carrinhoRepository = carrinhoRepository;
        _filmeRepository = filmeRepository;
        _filmeCompraRepository = filmeCompraRepository;
        _festivalRepository = festivalRepository;
        _notificationSubject = notificationSubject;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<IReadOnlyList<CarrinhoItem>> ListarItensAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var carrinho = await _carrinhoRepository.GetByUtilizadorIdAsync(utilizadorId, cancellationToken);

        return carrinho?.Itens
            .OrderByDescending(item => item.AdicionadoEmUtc)
            .ToList() ?? [];
    }

    public async Task<ServiceResult<string>> AdicionarAcessoFestivalAsync(
        int utilizadorId,
        int festivalId,
        TipoAcesso tipoAcesso,
        DateTime? diaFestival,
        int? horasAluguer,
        decimal preco,
        CancellationToken cancellationToken = default)
    {
        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);

        if (utilizador is null)
            return ServiceResult<string>.Failure("Utilizador nao encontrado.");

        var festival = await _festivalRepository.GetByIdAsync(festivalId, cancellationToken);

        if (festival is null)
            return ServiceResult<string>.Failure("Festival nao encontrado.");

        if (tipoAcesso == TipoAcesso.BilheteSessao)
            return ServiceResult<string>.Failure("Bilhetes de sessao serao adicionados a partir da pagina do filme.");

        var pedido = new AcessoPedido
        {
            Tipo = tipoAcesso,
            FestivalId = festival.Id,
            DiaFestival = diaFestival,
            InicioFestival = festival.DataInicio,
            FimFestival = festival.DataFim,
            HorasAluguer = horasAluguer,
            Preco = preco,
            CompradoEm = DateTime.Now
        };

        if (tipoAcesso == TipoAcesso.AluguerDigital)
            pedido.Decorators.Add(TipoAcessoDecorator.DuracaoAluguerDigital);

        var acesso = _acessoFactory.Criar(pedido);
        var janela = acesso.ObterJanelaAcesso();
        var carrinho = await ObterOuCriarCarrinhoAsync(utilizadorId, cancellationToken);

        await _carrinhoRepository.AddItemAsync(new CarrinhoItem
        {
            CarrinhoId = carrinho.Id,
            TipoAcesso = acesso.Tipo,
            FestivalId = festival.Id,
            DiaFestival = diaFestival,
            HorasAluguer = horasAluguer,
            Preco = preco,
            InicioAcessoUtc = janela.Inicio.ToUniversalTime(),
            FimAcessoUtc = janela.Fim.ToUniversalTime(),
            AdicionadoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Carrinho atualizado",
            Mensagem = $"{ObterDescricaoTipo(acesso.Tipo)} adicionado ao carrinho para {festival.Nome}.",
            Tipo = "carrinho"
        }, cancellationToken);

        return ServiceResult<string>.Success($"{ObterDescricaoTipo(acesso.Tipo)} adicionado ao carrinho.");
    }

    public async Task<ServiceResult<string>> AdicionarBilheteSessaoAsync(
        int utilizadorId,
        int filmeId,
        decimal preco,
        CancellationToken cancellationToken = default)
    {
        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);

        if (utilizador is null)
            return ServiceResult<string>.Failure("Utilizador nao encontrado.");

        var filme = await _filmeRepository.GetByIdAsync(filmeId, cancellationToken);

        if (filme is null)
            return ServiceResult<string>.Failure("Filme nao encontrado.");

        var inicioSessao = ObterInicioProximaSessaoIndividual();
        var fimSessao = inicioSessao.Add(DuracaoJanelaBilheteSessao);
        var pedido = new AcessoPedido
        {
            Tipo = TipoAcesso.BilheteSessao,
            FilmeId = filme.Id,
            SessaoId = SessaoEstaticaId,
            InicioSessao = inicioSessao,
            FimSessao = fimSessao,
            Preco = preco,
            CompradoEm = DateTime.Now
        };

        var acesso = _acessoFactory.Criar(pedido);
        var janela = acesso.ObterJanelaAcesso();
        var carrinho = await ObterOuCriarCarrinhoAsync(utilizadorId, cancellationToken);

        await _carrinhoRepository.AddItemAsync(new CarrinhoItem
        {
            CarrinhoId = carrinho.Id,
            TipoAcesso = acesso.Tipo,
            FilmeId = filme.Id,
            SessaoId = SessaoEstaticaId,
            Preco = preco,
            InicioAcessoUtc = janela.Inicio.ToUniversalTime(),
            FimAcessoUtc = janela.Fim.ToUniversalTime(),
            AdicionadoEmUtc = DateTime.UtcNow
        }, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Carrinho atualizado",
            Mensagem = $"Bilhete de sessao adicionado ao carrinho para {filme.Titulo}.",
            Tipo = "carrinho"
        }, cancellationToken);

        return ServiceResult<string>.Success("Bilhete de sessao adicionado ao carrinho.");
    }

    public async Task<ServiceResult<string>> FinalizarAcessosAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var carrinho = await _carrinhoRepository.GetByUtilizadorIdAsync(utilizadorId, cancellationToken);

        if (carrinho is null)
            return ServiceResult<string>.Failure("Carrinho nao encontrado.");

        var acessosAAtivar = carrinho.Itens
            .Where(item =>
                (item.TipoAcesso == TipoAcesso.BilheteSessao && item.FilmeId.HasValue) ||
                ((item.TipoAcesso == TipoAcesso.PasseDiario ||
                  item.TipoAcesso == TipoAcesso.PasseCompleto ||
                  item.TipoAcesso == TipoAcesso.AluguerDigital) && item.FestivalId.HasValue))
            .ToList();

        if (acessosAAtivar.Count == 0)
            return ServiceResult<string>.Success("Compra concluida. Nao existiam acessos suportados para ativar.");

        var acessosCriados = 0;

        foreach (var item in acessosAAtivar)
        {
            if (item.TipoAcesso == TipoAcesso.BilheteSessao)
            {
                await _filmeCompraRepository.AddAsync(new FilmeCompra
                {
                    UtilizadorId = utilizadorId,
                    FilmeId = item.FilmeId!.Value,
                    TipoAcesso = TipoAcesso.BilheteSessao,
                    InicioAcessoUtc = item.InicioAcessoUtc,
                    FimAcessoUtc = item.FimAcessoUtc,
                    CompradoEmUtc = DateTime.UtcNow
                }, cancellationToken);

                acessosCriados++;
            }
            else if (item.TipoAcesso == TipoAcesso.PasseDiario)
            {
                var diaFestival = item.InicioAcessoUtc.ToLocalTime().Date;

                foreach (var sessao in SessaoFestivalCatalog.SessoesPasseDiario)
                {
                    await _acessoFestivalCompraRepository.AddAsync(new AcessoFestivalCompra
                    {
                        UtilizadorId = utilizadorId,
                        FestivalId = item.FestivalId!.Value,
                        TipoAcesso = TipoAcesso.PasseDiario,
                        InicioAcessoUtc = sessao.ObterInicio(diaFestival).ToUniversalTime(),
                        FimAcessoUtc = sessao.ObterFim(diaFestival).ToUniversalTime(),
                        CompradoEmUtc = DateTime.UtcNow
                    }, cancellationToken);

                    acessosCriados++;
                }
            }
            else if (item.TipoAcesso == TipoAcesso.PasseCompleto)
            {
                var inicioFestival = item.InicioAcessoUtc.ToLocalTime().Date;
                var fimFestival = item.FimAcessoUtc.ToLocalTime().Date.AddDays(-1);

                for (var dia = inicioFestival; dia <= fimFestival; dia = dia.AddDays(1))
                {
                    foreach (var sessao in SessaoFestivalCatalog.SessoesPasseDiario)
                    {
                        await _acessoFestivalCompraRepository.AddAsync(new AcessoFestivalCompra
                        {
                            UtilizadorId = utilizadorId,
                            FestivalId = item.FestivalId!.Value,
                            TipoAcesso = TipoAcesso.PasseCompleto,
                            InicioAcessoUtc = sessao.ObterInicio(dia).ToUniversalTime(),
                            FimAcessoUtc = sessao.ObterFim(dia).ToUniversalTime(),
                            CompradoEmUtc = DateTime.UtcNow
                        }, cancellationToken);

                        acessosCriados++;
                    }
                }
            }
            else if (item.TipoAcesso == TipoAcesso.AluguerDigital)
            {
                var inicioAluguer = DateTime.Now;
                var fimAluguer = inicioAluguer.AddHours(item.HorasAluguer ?? 48);
                var festival = await _festivalRepository.GetByIdAsync(item.FestivalId!.Value, cancellationToken);

                if (festival is not null)
                {
                    var primeiroDia = inicioAluguer.Date < festival.DataInicio.Date
                        ? festival.DataInicio.Date
                        : inicioAluguer.Date;
                    var ultimoDia = fimAluguer.Date > festival.DataFim.Date
                        ? festival.DataFim.Date
                        : fimAluguer.Date;

                    for (var dia = primeiroDia; dia <= ultimoDia; dia = dia.AddDays(1))
                    {
                        foreach (var sessao in SessaoFestivalCatalog.SessoesPasseDiario)
                        {
                            var inicioSessao = sessao.ObterInicio(dia);
                            var fimSessao = sessao.ObterFim(dia);
                            var inicioCoberto = inicioAluguer > inicioSessao ? inicioAluguer : inicioSessao;
                            var fimCoberto = fimAluguer < fimSessao ? fimAluguer : fimSessao;

                            if (inicioCoberto >= fimCoberto)
                                continue;

                            await _acessoFestivalCompraRepository.AddAsync(new AcessoFestivalCompra
                            {
                                UtilizadorId = utilizadorId,
                                FestivalId = item.FestivalId.Value,
                                TipoAcesso = TipoAcesso.AluguerDigital,
                                InicioAcessoUtc = inicioCoberto.ToUniversalTime(),
                                FimAcessoUtc = fimCoberto.ToUniversalTime(),
                                CompradoEmUtc = DateTime.UtcNow
                            }, cancellationToken);

                            acessosCriados++;
                        }
                    }
                }
            }

            await _carrinhoRepository.RemoveItemAsync(item, cancellationToken);
        }

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Acesso ativado",
            Mensagem = "Os seus acessos ja estao disponiveis.",
            Tipo = "acesso"
        }, cancellationToken);

        return acessosCriados > 0
            ? ServiceResult<string>.Success("Compra concluida. Acesso(s) ativado(s).")
            : ServiceResult<string>.Success("Compra concluida. O festival ainda nao tinha filmes para ativar.");
    }

    public async Task<ServiceResult<string>> RemoverItemAsync(
        int utilizadorId,
        int itemId,
        CancellationToken cancellationToken = default)
    {
        var item = await _carrinhoRepository.GetItemByIdAsync(itemId, cancellationToken);

        if (item?.Carrinho is null || item.Carrinho.UtilizadorId != utilizadorId)
            return ServiceResult<string>.Failure("Item do carrinho nao encontrado.");

        await _carrinhoRepository.RemoveItemAsync(item, cancellationToken);

        await _notificationSubject.NotifyUserAsync(utilizadorId, new UserNotification
        {
            Titulo = "Carrinho atualizado",
            Mensagem = "Item removido do carrinho.",
            Tipo = "carrinho"
        }, cancellationToken);

        return ServiceResult<string>.Success("Item removido do carrinho.");
    }

    private async Task<Carrinho> ObterOuCriarCarrinhoAsync(
        int utilizadorId,
        CancellationToken cancellationToken)
    {
        var carrinho = await _carrinhoRepository.GetByUtilizadorIdAsync(utilizadorId, cancellationToken);

        if (carrinho is not null)
            return carrinho;

        carrinho = new Carrinho
        {
            UtilizadorId = utilizadorId,
            CriadoEmUtc = DateTime.UtcNow
        };

        await _carrinhoRepository.AddAsync(carrinho, cancellationToken);
        return carrinho;
    }

    private static string ObterDescricaoTipo(TipoAcesso tipoAcesso)
    {
        return tipoAcesso switch
        {
            TipoAcesso.PasseDiario => "Passe diario",
            TipoAcesso.PasseCompleto => "Passe completo",
            TipoAcesso.AluguerDigital => "Aluguer digital",
            TipoAcesso.BilheteSessao => "Bilhete de sessao",
            _ => "Acesso"
        };
    }

    private static DateTime ObterInicioProximaSessaoIndividual()
    {
        var agora = DateTime.Now;
        var inicioHoje = agora.Date.AddHours(HoraSessaoIndividualTeste).AddMinutes(MinutoSessaoIndividualTeste);
        var fimHoje = inicioHoje.Add(DuracaoJanelaBilheteSessao);

        return agora > fimHoje
            ? inicioHoje.AddDays(1)
            : inicioHoje;
    }
}
