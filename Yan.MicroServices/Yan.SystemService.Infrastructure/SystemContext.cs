using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.Infrastructure.Core;
using Yan.SystemService.Domain.Aggregate;
using Yan.SystemService.Domain.Entities;
using Yan.SystemService.Infrastructure.EntityConfigurations;

namespace Yan.SystemService.Infrastructure
{
    public class SystemContext : EFContext
    {
        public SystemContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new SystemUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SystemRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SystemMenuEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SystemRoleMenuEntityConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<SystemRole> SystemRoles { get; set; }

        public DbSet<SystemMenu> SystemMenus { get; set; }

        public DbSet<SystemRoleMenu> SystemRoleMenus { get; set; }
    }

}
