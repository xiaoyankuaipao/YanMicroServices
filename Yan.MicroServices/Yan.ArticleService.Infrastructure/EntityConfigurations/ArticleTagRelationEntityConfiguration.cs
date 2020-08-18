using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate;
using Yan.ArticleService.Domain.Entities;

namespace Yan.ArticleService.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleTagRelationEntityConfiguration : IEntityTypeConfiguration<ArticleTagRelation>
    {
        public void Configure(EntityTypeBuilder<ArticleTagRelation> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("ArticleTagRelation");

            builder.HasOne<Article>().WithMany(c=>c.ArticleTagRelations).HasForeignKey(c=>c.ArticleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<ArticleTag>().WithMany(c => c.ArticleTagRelations).HasForeignKey(c=>c.TagId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
