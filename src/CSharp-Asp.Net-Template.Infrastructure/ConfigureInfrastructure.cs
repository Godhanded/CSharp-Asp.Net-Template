using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Repository;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp_Asp.Net_Template.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastructureConfig(this IServiceCollection services, IConfiguration configs)
        {
            services.AddDbContext<AppDbContext>(opt =>
                        opt.UseNpgsql(
                            configs.GetConnectionString("Postgres"))
                        );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddSingleton(configs.GetRequiredSection("Jwt").Get<Jwt>()!);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthenticatedService, AuthenticatedService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            services.AddTransient<SeederService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
