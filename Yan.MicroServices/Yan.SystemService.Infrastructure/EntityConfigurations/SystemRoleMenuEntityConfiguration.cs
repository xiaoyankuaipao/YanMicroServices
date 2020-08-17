using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.SystemService.Domain.Aggregate;
using Yan.SystemService.Domain.Entities;

namespace Yan.SystemService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemRoleMenuEntityConfiguration : IEntityTypeConfiguration<SystemRoleMenu>
    {
        public void Configure(EntityTypeBuilder<SystemRoleMenu> builder)
        {
            builder.ToTable("SystemRoleMenu");
            builder.HasKey(p => p.Id);
            builder.HasOne<SystemRole>().WithMany(c => c.SystemRoleMenus).HasForeignKey(c => c.RoleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<SystemMenu>().WithMany(c => c.SystemRoleMenus).HasForeignKey(c => c.MenuId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
