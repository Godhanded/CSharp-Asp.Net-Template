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
            });
            return services;
        }
    }
}
