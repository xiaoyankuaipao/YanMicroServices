using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Buyering;
using Yan.BillService.Domain.Aggregate.Ordering;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(p => p.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
            });

            builder
               .Property<int?>("_buyerId")
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("BuyerId")
               .IsRequired(false);

            builder
                .Property<DateTime>("_orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            builder
                .Property<int?>("_paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId")
                .IsRequired(false);

            builder.Property<string>("Description").IsRequired(false);

            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("_paymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_buyerId");

        }
    }
}
