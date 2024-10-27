using CSharp_Asp.Net_Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharp_Asp.Net_Template.Domain.EntityConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.User)
                .WithMany(u => u.Invoices);
            builder.Property(i => i.CustomerId)
                .IsRequired();
            builder.Property(i => i.PeriodStart)
                .IsRequired();
            builder.Property(i => i.PeriodEnd)
                .IsRequired();
            builder.Property(i => i.CustomerEmail)
               .IsRequired();
            builder.Property(i => i.SubscriptionId)
               .IsRequired();
            builder.Property(i => i.ProductId)
               .IsRequired();

        }
    }
}
