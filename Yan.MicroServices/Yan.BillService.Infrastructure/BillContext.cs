using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Domain.Entities;
using Yan.BillService.Infrastructure.EntityConfigurations;
using Yan.Infrastructure.Core;

namespace Yan.BillService.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class BillContext : EFContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        /// <param name="capBus"></param>
        public BillContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new BillEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BillItemEntityConfiguration());

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Bill> Bills { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<BillItem> BillItems { get; set; }

    }
}
