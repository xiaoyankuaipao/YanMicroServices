using System;
using System.Collections.Generic;
using System.Text;
using Yan.Domain.Abstractions;
using Yan.Utility;

namespace Yan.ArticleService.Domain.Aggregate.MessageAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageAggregate:Entity<string>,IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; private set; }

        public MessageAggregate()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="imageUrl"></param>
        /// <param name="message"></param>
        public MessageAggregate(string userName, string imageUrl, string message)
        {
            this.Id= SnowflakeId.Default().NextId().ToString();
            this.UserName = userName;
            this.ImageUrl = imageUrl;
            this.Message = message;
            this.CreateTime = DateTime.Now;
        }

    }
}
