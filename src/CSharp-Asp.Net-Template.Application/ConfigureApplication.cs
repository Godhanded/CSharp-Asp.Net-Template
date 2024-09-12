using CSharp_Asp.Net_Template.Application.Shared.Dtos;
using CSharp_Asp.Net_Template.Infrastructure.Services;
using CSharp_Asp.Net_Template.Infrastructure.Utilities.ConfigurationSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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

            services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = ctx =>
                                                               GetCustomInvalidStateResponseFactory(ctx);
                });
            return services;
        }

        private static IActionResult GetCustomInvalidStateResponseFactory(ActionContext ctx)
        {
            var errors = ctx.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .Select(e => new ModelStateErrorDto
                            {
                                Field = e.Key,
                                Message = e.Value!.Errors.First().ErrorMessage
                            })
                            .ToList();
            return new BadRequestObjectResult(new ModelStateErrorResponseDto { Errors = errors });
        }
    }
}
