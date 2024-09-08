using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Repository;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp_Asp.Net_Template.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastuctureConfig(this IServiceCollection services, IConfiguration configs)
        {
            services.AddDbContext<AppDbContext>(opt=>
                        opt.UseNpgsql(
                            configs.GetConnectionString("Postgres"))
                        );
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            return services;
        }
    }
}
