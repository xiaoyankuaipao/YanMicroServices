using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.BillService.Domain.Aggregate;
using Yan.BillService.Domain.Aggregate.Buyering;
using Yan.BillService.Domain.Aggregate.Ordering;
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

            modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());

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

        public DbSet<CardType> CardTypes { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}
