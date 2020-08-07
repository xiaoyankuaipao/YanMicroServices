using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;

namespace Yan.ArticleService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleEntityConfiguration : IEntityTypeConfiguration<Article>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Articles");
            builder.Property(p => p.Title).HasMaxLength(255);
            builder.Ignore(p => p.TagIds);
        }
    }
}
