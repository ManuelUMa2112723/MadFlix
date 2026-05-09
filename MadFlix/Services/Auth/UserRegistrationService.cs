using System.ComponentModel.DataAnnotations;
using MadFlix.Contracts.Common;
using MadFlix.Models;
using MadFlix.Repositories;

namespace MadFlix.Services.Auth;

public sealed class UserRegistrationService : IUserRegistrationService
{
    private readonly EmailAddressAttribute _emailAddressValidator = new();
    private readonly IUserIdentityNormalizer _identityNormalizer;
    private readonly IPasswordVerificationService _passwordVerificationService;
    private readonly IUtilizadorRepository _utilizadorRepository;

    public UserRegistrationService(
        IUserIdentityNormalizer identityNormalizer,
        IPasswordVerificationService passwordVerificationService,
        IUtilizadorRepository utilizadorRepository)
    {
        _identityNormalizer = identityNormalizer;
        _passwordVerificationService = passwordVerificationService;
        _utilizadorRepository = utilizadorRepository;
    }

    public async Task<ServiceResult<Utilizador>> RegisterAsync(
        string nomeUtilizador,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var nomeLimpo = _identityNormalizer.Trim(nomeUtilizador);
        var emailLimpo = _identityNormalizer.Trim(email);

        if (string.IsNullOrWhiteSpace(nomeLimpo))
        {
            return ServiceResult<Utilizador>.Failure("O nome de utilizador e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(emailLimpo))
        {
            return ServiceResult<Utilizador>.Failure("O email e obrigatorio.");
        }

        if (!_emailAddressValidator.IsValid(emailLimpo))
        {
            return ServiceResult<Utilizador>.Failure("Introduza um email valido.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            return ServiceResult<Utilizador>.Failure("A password tem de ter pelo menos 6 caracteres.");
        }

        var nomeNormalizado = _identityNormalizer.Normalize(nomeLimpo);
        var emailNormalizado = _identityNormalizer.Normalize(emailLimpo);

        if (await _utilizadorRepository.ExisteNomeUtilizadorAsync(nomeNormalizado, cancellationToken))
        {
            return ServiceResult<Utilizador>.Failure("Esse nome de utilizador ja existe.");
        }

        if (await _utilizadorRepository.ExisteEmailAsync(emailNormalizado, cancellationToken))
        {
            return ServiceResult<Utilizador>.Failure("Esse email ja esta associado a outra conta.");
        }

        var utilizador = new Utilizador
        {
            NomeUtilizador = nomeLimpo,
            NomeUtilizadorNormalizado = nomeNormalizado,
            Email = emailLimpo,
            EmailNormalizado = emailNormalizado,
            CriadoEmUtc = DateTime.UtcNow
        };

        utilizador.PasswordHash = _passwordVerificationService.HashPassword(utilizador, password);

        await _utilizadorRepository.AddAsync(utilizador, cancellationToken);

        return ServiceResult<Utilizador>.Success(utilizador);
    }
}
