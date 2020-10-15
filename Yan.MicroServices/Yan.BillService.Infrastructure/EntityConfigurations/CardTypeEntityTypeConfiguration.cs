using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate.Buyering;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    public class CardTypeEntityTypeConfiguration : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> builder)
        {
            builder.ToTable("cardtypes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
        }

    }
}
