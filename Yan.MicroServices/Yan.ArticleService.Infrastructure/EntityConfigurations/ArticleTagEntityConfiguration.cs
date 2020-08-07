using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.ArticleTagAggregate;

namespace Yan.ArticleService.Infrastructure.EntityConfigurations
{
    public class ArticleTagEntityConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("ArticleTag");
        }
    }
}
