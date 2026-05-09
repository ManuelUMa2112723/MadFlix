using MadFlix.Contracts.Common;
using MadFlix.Models;

namespace MadFlix.Services.Auth;

public interface IUserRegistrationService
{
    Task<ServiceResult<Utilizador>> RegisterAsync(
        string nomeUtilizador,
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
