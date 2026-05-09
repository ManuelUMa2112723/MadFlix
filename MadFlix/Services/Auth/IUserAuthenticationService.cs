using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Auth;

public interface IUserAuthenticationService
{
    Task<ServiceResult<Utilizador>> AuthenticateAsync(
        string identificador,
        string password,
        CancellationToken cancellationToken = default);
}
