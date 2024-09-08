using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Domain.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Asp.Net_Template.Infrastructure.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<User>(new UserConfigurations());
        }

        public DbSet<User> Users { get; set; }
    }
}
