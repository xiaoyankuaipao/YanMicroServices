using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.ArticleService.Infrastructure.EntityConfigurations;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleContext:EFContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        /// <param name="capBus"></param>
        public ArticleContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus)
            : base(options, mediator, capBus)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new ArticleCategoryEntityConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
    }


}
