using MadFlix.Contracts.Common;
using MadFlix.Contracts.Profile;
using MadFlix.Repositories;

namespace MadFlix.Services.Profile;

public sealed class UserProfilePhotoService : IUserProfilePhotoService
{
    private static readonly IReadOnlyDictionary<string, string> AllowedExtensionsByContentType =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["image/jpeg"] = ".jpg",
            ["image/png"] = ".png",
            ["image/webp"] = ".webp"
        };

    private readonly IProfilePhotoStorage _photoStorage;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public UserProfilePhotoService(
        IProfilePhotoStorage photoStorage,
        IUtilizadorRepository utilizadorRepository)
    {
        _photoStorage = photoStorage;
        _utilizadorRepository = utilizadorRepository;
    }

    public long MaxFileSizeBytes => 2 * 1024 * 1024;

    public async Task<ServiceResult<string>> ChangePhotoAsync(
        int utilizadorId,
        ProfilePhotoUpload upload,
        CancellationToken cancellationToken = default)
    {
        if (upload.Size <= 0)
            return ServiceResult<string>.Failure("Escolha uma imagem valida.");

        if (upload.Size > MaxFileSizeBytes)
            return ServiceResult<string>.Failure("A imagem deve ter no maximo 2 MB.");

        if (!AllowedExtensionsByContentType.TryGetValue(upload.ContentType, out var extension))
            return ServiceResult<string>.Failure("Use uma imagem JPG, PNG ou WebP.");

        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);

        if (utilizador is null)
            return ServiceResult<string>.Failure("Utilizador nao encontrado.");

        var previousPhotoUrl = utilizador.FotoPerfilUrl;
        var newPhotoUrl = await _photoStorage.SaveAsync(upload.Content, extension, cancellationToken);

        utilizador.FotoPerfilUrl = newPhotoUrl;
        await _utilizadorRepository.UpdateAsync(utilizador, cancellationToken);
        await _photoStorage.DeleteAsync(previousPhotoUrl, cancellationToken);

        return ServiceResult<string>.Success(newPhotoUrl);
    }

    public async Task<string?> GetPhotoUrlAsync(
        int utilizadorId,
        CancellationToken cancellationToken = default)
    {
        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);
        return utilizador?.FotoPerfilUrl;
    }
}
