using MadFlix.Data;
using MadFlix.Repositories;
using MadFlix.Services;
using MadFlix.Services.Auth;
using MadFlix.Services.Carrinhos;
using MadFlix.Services.Festivais;
using MadFlix.Services.Filmes;
using MadFlix.Services.Notifications;
using MadFlix.Models.Acessos.FactoriesAcessos;
using MadFlix.Models.Acessos.InterfaceAcessos;
using MadFlix.Models.Festivais.FactoriesFestivais;
using MadFlix.Models.Festivais.InterfaceFestivais;
using MadFlix.Models.Festivais.ObserverFestivais;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMadFlixAuthentication(this IServiceCollection services)
    {
        services.AddCascadingAuthenticationState();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "MadFlix.Auth";
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/login";
                options.SlidingExpiration = true;
            });
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddMadFlixDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IFilmeRepository, EfFilmeRepository>();
        services.AddScoped<IUtilizadorRepository, EfUtilizadorRepository>();
        services.AddScoped<IFilmeCompraRepository, EfFilmeCompraRepository>();
        services.AddScoped<IFilmeVisualizacaoRepository, EfFilmeVisualizacaoRepository>();
        services.AddScoped<IFestivalRepository, EfFestivalRepository>();
        services.AddScoped<ICarrinhoRepository, EfCarrinhoRepository>();
        services.AddScoped<IComentarioFilmeRepository, EfComentarioFilmeRepository>();

        return services;
    }

    public static IServiceCollection AddMadFlixApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddScoped<IFestivalFactory, FestivalFactory>();
        services.AddScoped<IAcessoFactory, AcessoFactoryConcrete>();
        services.AddScoped<FilmeService>();

        services.AddScoped<IUserIdentityNormalizer, UserIdentityNormalizer>();
        services.AddScoped<IPasswordVerificationService, PasswordVerificationService>();
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<IUserClaimsPrincipalFactory, UserClaimsPrincipalFactory>();
        services.AddScoped<IUserSessionService, UserSessionService>();

        services.AddScoped<IFilmeCatalogService, FilmeCatalogService>();
        services.AddScoped<IFilmePurchaseService, FilmePurchaseService>();
        services.AddScoped<IFilmePlaybackService, FilmePlaybackService>();
        services.AddScoped<IComentarioFilmeService, ComentarioFilmeService>();

        services.AddScoped<IFestivalCatalogService, FestivalCatalogService>();
        services.AddScoped<IFestivalCreationService, FestivalCreationService>();
        services.AddScoped<IFestivalMovieService, FestivalMovieService>();
        services.AddScoped<ICarrinhoService, CarrinhoService>();

        services.AddSingleton<IUserNotificationSubject, UserNotificationDispatcher>();
        services.AddSingleton<IFestivalSubject, FestivalSubject>();
        services.AddSingleton<IFestivalObserver, FestivalNotificationObserver>();
        services.AddScoped<IUserNotificationFeed, UserNotificationCenter>();

        return services;
    }
}
