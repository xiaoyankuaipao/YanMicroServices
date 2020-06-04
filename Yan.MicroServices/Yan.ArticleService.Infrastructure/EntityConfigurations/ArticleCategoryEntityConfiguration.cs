using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;

namespace Yan.ArticleService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 针对每一个领域模型，创建一个EntityTypeConfiguration
    /// </summary>
    public class ArticleCategoryEntityConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("ArticleCategory");
            builder.Property(p => p.Category).HasMaxLength(255);
        }
    }
}
