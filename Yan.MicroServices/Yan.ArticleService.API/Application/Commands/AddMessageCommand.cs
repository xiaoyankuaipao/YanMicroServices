using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.Domain.Aggregate.MessageAggregate;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Core.Dtos;

namespace Yan.ArticleService.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class AddMessageCommand:IRequest<HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, HandleResultDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// 
        /// </summary>
        public AddMessageCommandHandler(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HandleResultDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new MessageAggregate(request.UserName, request.ImgUrl, request.Message);
            await this._messageRepository.AddAsync(message, cancellationToken);
            await this._messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new HandleResultDto
            {
                State = 1
            };
        }
    }
}
