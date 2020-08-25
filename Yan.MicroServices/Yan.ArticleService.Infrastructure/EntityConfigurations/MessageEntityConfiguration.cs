using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.MessageAggregate;

namespace Yan.ArticleService.Infrastructure.EntityConfigurations
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageAggregate>
    {
        public void Configure(EntityTypeBuilder<MessageAggregate> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(p => p.Id);
        }
    }
}
