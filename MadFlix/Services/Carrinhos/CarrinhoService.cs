using MadFlix.Contracts.Common;
using MadFlix.Factories;
using MadFlix.Models;
using MadFlix.Models.Acessos;
using MadFlix.Repositories;
using MadFlix.Services.Notifications;

namespace MadFlix.Services.Carrinhos;

public sealed class CarrinhoService : ICarrinhoService
{
    private readonly IAcessoFactory _acessoFactory;
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IFestivalRepository _festivalRepository;
    private readonly IUserNotificationSubject _notificationSubject;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public CarrinhoService(
        IAcessoFactory acessoFactory,
        ICarrinhoRepository carrinhoRepository,
        IFestivalRepository festivalRepository,
        IUserNotificationSubject notificationSubject,
        IUtilizadorRepository utilizadorRepository)
    {
        _acessoFactory = acessoFactory;
        _carrinhoRepository = carrinhoRepository;
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
}
