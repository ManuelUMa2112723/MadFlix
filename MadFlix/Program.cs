using MadFlix.Components;
using MadFlix.DependencyInjection;
using MadFlix.Endpoints;
using MadFlix.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMadFlixAuthentication();
builder.Services.AddMadFlixDataAccess(builder.Configuration);
builder.Services.AddMadFlixApplicationServices(builder.Configuration);

var app = builder.Build();

if (builder.Configuration.GetValue("Database:ApplyMigrationsOnStartup", false))
{
    app.ApplyMadFlixMigrations();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapAuthEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
