using MadFlix.Data;
using Microsoft.EntityFrameworkCore;

namespace MadFlix.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ApplyMadFlixMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}
