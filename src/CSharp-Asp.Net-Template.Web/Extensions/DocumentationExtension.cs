using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CSharp_Asp.Net_Template.Web.Extensions
{
    public static class DocumentationExtension
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(genOpt =>
            {
                genOpt.SwaggerDoc("v1", new OpenApiInfo { Title = "Asp.Net Core Api Boiler plate", Version = "v1", Description = "" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                genOpt.IncludeXmlComments(xmlPath);

                genOpt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                                    "\r\n\r\nExample: \"Bearer YqNHJIiokdjeopDlkw\""
                });

                genOpt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<String>()
                    }
                });
            });
            return services;
        }
    }
}
