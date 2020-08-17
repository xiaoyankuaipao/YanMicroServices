using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.SystemService.Domain.Aggregate;

namespace Yan.SystemService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemMenuEntityConfiguration : IEntityTypeConfiguration<SystemMenu>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SystemMenu> builder)
        {
            builder.ToTable("SystemMenu");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Code).HasMaxLength(255);
            builder.Property(p => p.Address).HasMaxLength(255); 
            builder.Property(p => p.Icon).HasMaxLength(255);
            builder.Property(p => p.ParentId).HasMaxLength(255);

            builder.HasOne<SystemMenu>().WithMany().IsRequired(false).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
