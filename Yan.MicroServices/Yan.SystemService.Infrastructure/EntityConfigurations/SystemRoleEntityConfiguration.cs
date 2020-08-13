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
    public class SystemRoleEntityConfiguration : IEntityTypeConfiguration<SystemRole>
    {
        public void Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable("SystemRole");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.DisplayName).HasMaxLength(255);
            builder.HasMany(p => p.SystemRoleMenus);
        }
    }

}
