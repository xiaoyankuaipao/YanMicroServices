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
    public class SystemUserEntityConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.ToTable("SystemUser");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserName).HasMaxLength(255);
            builder.Property(p => p.RealName).HasMaxLength(255);
            builder.Property(p => p.Email).HasMaxLength(255);
        }
    }
}
