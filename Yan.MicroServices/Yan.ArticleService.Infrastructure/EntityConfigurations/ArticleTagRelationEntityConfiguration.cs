using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
