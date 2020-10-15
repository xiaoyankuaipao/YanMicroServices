using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Buyering;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("buyers");
            builder.HasKey(c => c.Id);
            builder.Ignore(c => c.DomainEvents);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.IdentityGuid)
               .HasMaxLength(200)
               .IsRequired();

            builder.HasIndex("IdentityGuid")
              .IsUnique(true);

            builder.Property(b => b.Name);

            builder.HasMany(b => b.PaymentMethods)
                .WithOne()
                .HasForeignKey("BuyerId")
                .OnDelete(DeleteBehavior.Cascade);

            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
