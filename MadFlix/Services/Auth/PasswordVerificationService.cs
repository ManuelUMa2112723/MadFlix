using MadFlix.Models;
using Microsoft.AspNetCore.Identity;

namespace MadFlix.Services.Auth;

public sealed class PasswordVerificationService : IPasswordVerificationService
{
    private readonly PasswordHasher<Utilizador> _passwordHasher = new();

    public string HashPassword(Utilizador utilizador, string password)
    {
        return _passwordHasher.HashPassword(utilizador, password);
    }

    public PasswordVerificationStatus VerifyPassword(Utilizador utilizador, string passwordHash, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(utilizador, passwordHash, password);

        return result switch
        {
            PasswordVerificationResult.Success => PasswordVerificationStatus.Succeeded,
            PasswordVerificationResult.SuccessRehashNeeded => PasswordVerificationStatus.SucceededRehashRequired,
            _ => PasswordVerificationStatus.Failed
        };
    }
}
