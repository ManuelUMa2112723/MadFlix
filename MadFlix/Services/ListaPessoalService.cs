using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services;

public sealed class ListaPessoalService : IListaPessoalService
{
    private readonly IListaPessoalRepository _repository;

    public ListaPessoalService(IListaPessoalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ListaPessoal>> ListarAsync(int utilizadorId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByUtilizadorIdAsync(utilizadorId, cancellationToken);
    }

    public async Task<ServiceResult<ListaPessoal>> CriarAsync(int utilizadorId, string nome, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return ServiceResult<ListaPessoal>.Failure("O nome da lista e obrigatorio.");

        var lista = new ListaPessoal
        {
            Nome = nome.Trim(),
            UtilizadorId = utilizadorId
        };

        await _repository.AddAsync(lista, cancellationToken);
        return ServiceResult<ListaPessoal>.Success(lista);
    }

    public async Task<ServiceResult> EliminarAsync(int listaId, int utilizadorId, CancellationToken cancellationToken = default)
    {
        var lista = await _repository.GetByIdAsync(listaId, cancellationToken);

        if (lista is null)
            return ServiceResult.Failure("Lista nao encontrada.");

        if (lista.UtilizadorId != utilizadorId)
            return ServiceResult.Failure("Nao tem permissoes para eliminar esta lista.");

        await _repository.DeleteAsync(lista, cancellationToken);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> AdicionarFilmeAsync(int listaId, int filmeId, int utilizadorId, CancellationToken cancellationToken = default)
    {
        var lista = await _repository.GetByIdAsync(listaId, cancellationToken);

        if (lista is null)
            return ServiceResult.Failure("Lista nao encontrada.");

        if (lista.UtilizadorId != utilizadorId)
            return ServiceResult.Failure("Nao tem permissoes para editar esta lista.");

        var filmeExistente = await _repository.GetFilmeAsync(listaId, filmeId, cancellationToken);

        if (filmeExistente is not null)
            return ServiceResult.Failure("Este filme ja esta na lista.");

        await _repository.AddFilmeAsync(new ListaPessoalFilme
        {
            ListaPessoalId = listaId,
            FilmeId = filmeId
        }, cancellationToken);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> RemoverFilmeAsync(int listaId, int filmeId, int utilizadorId, CancellationToken cancellationToken = default)
    {
        var lista = await _repository.GetByIdAsync(listaId, cancellationToken);

        if (lista is null)
            return ServiceResult.Failure("Lista nao encontrada.");

        if (lista.UtilizadorId != utilizadorId)
            return ServiceResult.Failure("Nao tem permissoes para editar esta lista.");

        var filme = await _repository.GetFilmeAsync(listaId, filmeId, cancellationToken);

        if (filme is null)
            return ServiceResult.Failure("Este filme nao esta na lista.");

        await _repository.RemoveFilmeAsync(filme, cancellationToken);
        return ServiceResult.Success();
    }
}