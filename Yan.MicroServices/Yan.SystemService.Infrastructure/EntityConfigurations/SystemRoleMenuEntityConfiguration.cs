using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
