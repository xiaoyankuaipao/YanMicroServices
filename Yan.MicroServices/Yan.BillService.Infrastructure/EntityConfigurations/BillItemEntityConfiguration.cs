using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Domain.Entities;

namespace Yan.BillService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class BillItemEntityConfiguration : IEntityTypeConfiguration<BillItem>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<BillItem> builder)
        {
            builder.ToTable("BillItem");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Remark).HasMaxLength(255);

            builder.HasOne<Bill>().WithMany(p => p.BillItems).IsRequired(true).HasForeignKey(p => p.BillId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
