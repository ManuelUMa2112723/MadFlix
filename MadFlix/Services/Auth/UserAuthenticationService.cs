using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Auth;

public sealed class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserIdentityNormalizer _identityNormalizer;
    private readonly IPasswordVerificationService _passwordVerificationService;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public UserAuthenticationService(
        IUserIdentityNormalizer identityNormalizer,
        IPasswordVerificationService passwordVerificationService,
        IUtilizadorRepository utilizadorRepository)
    {
        _identityNormalizer = identityNormalizer;
        _passwordVerificationService = passwordVerificationService;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<ServiceResult<Utilizador>> AuthenticateAsync(
        string identificador,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(identificador) || string.IsNullOrWhiteSpace(password))
        {
            return ServiceResult<Utilizador>.Failure("Credenciais invalidas.");
        }

        var identificadorNormalizado = _identityNormalizer.Normalize(identificador);
        var utilizador = await _utilizadorRepository.GetByIdentidadeNormalizadaAsync(
            identificadorNormalizado,
            cancellationToken);

        if (utilizador is null)
        {
            return ServiceResult<Utilizador>.Failure("Credenciais invalidas.");
        }

        var verificationStatus = _passwordVerificationService.VerifyPassword(
            utilizador,
            utilizador.PasswordHash,
            password);

        if (verificationStatus == PasswordVerificationStatus.Failed)
        {
            return ServiceResult<Utilizador>.Failure("Credenciais invalidas.");
        }

        if (verificationStatus == PasswordVerificationStatus.SucceededRehashRequired)
        {
            utilizador.PasswordHash = _passwordVerificationService.HashPassword(utilizador, password);
            await _utilizadorRepository.UpdateAsync(utilizador, cancellationToken);
        }

        return ServiceResult<Utilizador>.Success(utilizador);
    }
}
