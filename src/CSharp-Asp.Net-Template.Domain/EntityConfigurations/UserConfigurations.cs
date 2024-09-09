using CSharp_Asp.Net_Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharp_Asp.Net_Template.Domain.EntityConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.FirstName)
                .IsRequired();
            builder.Property(u => u.LastName)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired();
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.PasswordSalt)
                .IsRequired();
            builder.Property(u => u.Status)
                .HasConversion<string>();
        }
    }
}
