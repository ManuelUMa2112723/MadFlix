using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Auth;

public sealed class UserPasswordService : IUserPasswordService
{
    private readonly IPasswordVerificationService _passwordVerificationService;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public UserPasswordService(
        IPasswordVerificationService passwordVerificationService,
        IUtilizadorRepository utilizadorRepository)
    {
        _passwordVerificationService = passwordVerificationService;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<ServiceResult> ChangePasswordAsync(
        int utilizadorId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
        {
            return ServiceResult.Failure("Preencha todos os campos.");
        }

        if (newPassword.Length < 6)
        {
            return ServiceResult.Failure("A password tem de ter pelo menos 6 caracteres.");
        }

        var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId, cancellationToken);
        if (utilizador is null)
        {
            return ServiceResult.Failure("Utilizador nao encontrado.");
        }

        var verificationStatus = _passwordVerificationService.VerifyPassword(
            utilizador,
            utilizador.PasswordHash,
            currentPassword);

        if (verificationStatus == PasswordVerificationStatus.Failed)
        {
            return ServiceResult.Failure("Password atual incorreta.");
        }

        utilizador.PasswordHash = _passwordVerificationService.HashPassword(utilizador, newPassword);
        await _utilizadorRepository.UpdateAsync(utilizador, cancellationToken);
        return ServiceResult.Success();
    }
}
