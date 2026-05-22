using System.Security.Claims;
using MadFlix.Contracts.Auth;
using MadFlix.Extensions;
using MadFlix.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MadFlix.Endpoints;

public static class AuthEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/auth/login", LoginAsync);
        endpoints.MapPost("/auth/register", RegisterAsync);
        endpoints.MapPost("/auth/logout", LogoutAsync);
        endpoints.MapPost("/auth/change-password", ChangePasswordAsync).RequireAuthorization();

        return endpoints;
    }

    private static async Task<IResult> LoginAsync(
        [FromForm] LoginRequest request,
        IUserAuthenticationService authenticationService,
        IUserSessionService sessionService,
        HttpContext httpContext)
    {
        var result = await authenticationService.AuthenticateAsync(request.Identificador, request.Password);

        if (!result.Succeeded || result.Value is null)
        {
            return Results.LocalRedirect(BuildRedirect("/login", result.ErrorMessage ?? "Credenciais invalidas.", request.ReturnUrl));
        }

        await sessionService.SignInAsync(httpContext, result.Value);

        return Results.LocalRedirect("/");
    }

    private static async Task<IResult> RegisterAsync(
        [FromForm] RegisterRequest request,
        IUserRegistrationService registrationService,
        IUserSessionService sessionService,
        HttpContext httpContext)
    {
        if (request.Password != request.ConfirmarPassword)
        {
            return Results.LocalRedirect(BuildRedirect("/registar", "As passwords nao coincidem.", request.ReturnUrl));
        }

        var result = await registrationService.RegisterAsync(
            request.NomeUtilizador,
            request.Email,
            request.Password);

        if (!result.Succeeded || result.Value is null)
        {
            return Results.LocalRedirect(BuildRedirect("/registar", result.ErrorMessage ?? "Nao foi possivel criar a conta.", request.ReturnUrl));
        }

        await sessionService.SignInAsync(httpContext, result.Value);

        return Results.LocalRedirect("/");
    }

    private static async Task<IResult> LogoutAsync(
        [FromForm] LogoutRequest request,
        IUserSessionService sessionService,
        HttpContext httpContext)
    {
        await sessionService.SignOutAsync(httpContext);
        return Results.LocalRedirect(NavigationHelpers.NormalizeReturnUrl(request.ReturnUrl));
    }

    private static async Task<IResult> ChangePasswordAsync(
        [FromForm] ChangePasswordRequest request,
        IUserPasswordService passwordService,
        HttpContext httpContext)
    {
        if (!httpContext.User.IsInRole("Admin"))
        {
            return Results.Json(new { error = "Apenas administradores podem alterar a password." }, statusCode: 403);
        }

        var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(idClaim, out var utilizadorId))
        {
            return Results.Unauthorized();
        }

        if (request.Password != request.ConfirmarPassword)
        {
            return Results.BadRequest("As passwords nao coincidem.");
        }

        var result = await passwordService.ChangePasswordAsync(
            utilizadorId,
            request.CurrentPassword,
            request.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.ErrorMessage ?? "Nao foi possivel alterar a password.");
        }

        return Results.Ok();
    }

    private static string BuildRedirect(string basePath, string error, string? returnUrl)
    {
        var safeError = Uri.EscapeDataString(error);
        var safeReturnUrl = Uri.EscapeDataString(NavigationHelpers.NormalizeReturnUrl(returnUrl));
        return $"{basePath}?error={safeError}&returnUrl={safeReturnUrl}";
    }
}
