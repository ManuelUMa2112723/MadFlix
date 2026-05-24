using MadFlix.Contracts.Common;
using MadFlix.Contracts.Profile;

namespace MadFlix.Services.Profile;

public interface IUserProfilePhotoService
{
    long MaxFileSizeBytes { get; }

    Task<ServiceResult<string>> ChangePhotoAsync(
        int utilizadorId,
        ProfilePhotoUpload upload,
        CancellationToken cancellationToken = default);

    Task<string?> GetPhotoUrlAsync(int utilizadorId, CancellationToken cancellationToken = default);
}
