using CSharp_Asp.Net_Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharp_Asp.Net_Template.Domain.EntityConfigurations
{
    public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(ut => ut.Token)
                .IsRequired();
            builder.Property(ut => ut.ExpirationDate)
                .IsRequired();
            builder.Property(ut => ut.UserId)
                .IsRequired();
            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTokens);
        }
    }
}
