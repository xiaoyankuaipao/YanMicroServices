using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Ordering;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    class OrderItemEntityTypeConfiguration
        : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("orderitems");

            builder.HasKey(p => p.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            //https://docs.microsoft.com/zh-cn/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
            builder.Property<int>("OrderId")
                .IsRequired();

            builder.Property<string>("abc");


            builder
                .Property<decimal>("_discount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Discount")
                .IsRequired();

            builder.Property<int>("ProductId")
                .IsRequired();

            builder
                .Property<string>("_productName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ProductName")
                .IsRequired();

            builder
                .Property<decimal>("_unitPrice")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UnitPrice")
                .IsRequired();

            builder
                .Property<int>("_units")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Units")
                .IsRequired();

            builder
                .Property<string>("_pictureUrl")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PictureUrl")
                .IsRequired(false);

        }
    }
}
