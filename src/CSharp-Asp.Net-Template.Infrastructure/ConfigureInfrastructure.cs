using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Repository;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Services;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;


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

            services.AddOptions<JwtOptions>()
                .BindConfiguration("Jwt")
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.Configure<MailOptions>(configs.GetSection("Mail"));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthenticatedService, AuthenticatedService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<SeederService>();

            services.AddSingleton<RazorLightEngine>(sp => new RazorLightEngineBuilder()
                   .UseEmbeddedResourcesProject(typeof(ConfigureInfrastructure)) // or UseFileSystemProject if loading from the filesystem
                   .SetOperatingAssembly(typeof(EmailService).Assembly)
                   .UseMemoryCachingProvider()
                   .EnableDebugMode(true)
                   .Build()
            );

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
