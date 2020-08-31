using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class BillEntityConfiguration : IEntityTypeConfiguration<Bill>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");
            builder.Property(t => t.BillName).HasMaxLength(255).IsRequired();
            builder.Property(t => t.Person).HasMaxLength(255).IsRequired();

            builder.HasKey(p => p.Id);
        }
    }
}
