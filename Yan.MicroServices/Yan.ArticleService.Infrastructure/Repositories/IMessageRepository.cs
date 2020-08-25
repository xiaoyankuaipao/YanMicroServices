using System;
using System.Collections.Generic;
using System.Text;
using Yan.ArticleService.Domain.Aggregate.MessageAggregate;
using Yan.Infrastructure.Core;

namespace Yan.ArticleService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageRepository:IRepository<MessageAggregate,string>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class MessageRepository : Repository<MessageAggregate, string, ArticleContext>, IMessageRepository
    {
        public MessageRepository(ArticleContext context) : base(context)
        {
        }
    }
}
