using MadFlix.Contracts.Common;

namespace MadFlix.Services.Auth;

public interface IUserPasswordService
{
    Task<ServiceResult> ChangePasswordAsync(
        int utilizadorId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default);
}
