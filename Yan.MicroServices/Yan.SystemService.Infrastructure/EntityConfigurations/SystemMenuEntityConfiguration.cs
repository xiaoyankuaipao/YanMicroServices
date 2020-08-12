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
        }
    }
}
