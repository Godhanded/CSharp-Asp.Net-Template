using CSharp_Asp.Net_Template.Infrastructure.Services;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CSharp_Asp.Net_Template.Application
{
    public static class ConfigureApplication
    {
        public static IServiceCollection AddApplicationConfig(this IServiceCollection services, IConfiguration configs)
        {
            services.AddMediatR(cf =>
                                    cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCors(corsOpt =>
            {
                corsOpt.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOpt =>
            {
                jwtOpt.TokenValidationParameters = TokenService.GetTokenValidationParameters(configs.GetSection("Jwt")
                    .Get<Jwt>()!.SecretKey);
            });

            services.AddAuthorization();
            return services;
        }
    }
}
