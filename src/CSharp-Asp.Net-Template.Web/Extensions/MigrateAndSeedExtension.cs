using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Asp.Net_Template.Web.Extensions
{
    public static class MigrateAndSeedExtension
    {

        public static async Task<WebApplication> MigrateAndSeedDataBase(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<SeederService>();
            var dbCtx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbCtx.Database.Migrate();
            await seeder.SeedUsers();

            return app;
        }
    }
}
