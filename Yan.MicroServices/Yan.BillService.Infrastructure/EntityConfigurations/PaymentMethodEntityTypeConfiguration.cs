using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Buyering;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("paymentmethods");
            builder.HasKey(b => b.Id);
            builder.Ignore(c => c.DomainEvents);
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property<int>("BuyerId")
                .IsRequired();

            builder.Property<string>("_cardHolderName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardHolderName")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property<string>("_alias")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Alias")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property<string>("_cardNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardNumber")
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property<DateTime>("_expiration")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Expiration")
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property<int>("_cardTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardTypeId")
                .IsRequired();

            builder.HasOne(p => p.CardType)
                .WithMany()
                .HasForeignKey("_cardTypeId");

        }
    }
}
