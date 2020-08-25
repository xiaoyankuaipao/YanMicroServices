using AutoMapper;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Domain.Aggregate.MessageAggregate;
using Yan.Core.Dtos;
using Yan.Dapper;

namespace Yan.ArticleService.API.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class MessagePageQuery:IRequest<PageResultDto<MessageOutputDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MessagePageQueryHandler : IRequestHandler<MessagePageQuery, PageResultDto<MessageOutputDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DapperHelper _dapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dapper"></param>
        public MessagePageQueryHandler(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResultDto<MessageOutputDto>> Handle(MessagePageQuery request, CancellationToken cancellationToken)
        {
            var sqlBuilder = new StringBuilder(@"select SQL_CALC_FOUND_ROWS Id,UserName,ImageUrl,Message,CreateTime from Message limit @skip,@take ;");
            sqlBuilder.Append("SELECT FOUND_ROWS() as Total;");

            var messages = await _dapper.QueryPage<MessageOutputDto>(sqlBuilder.ToString(), new { skip = (request.Page - 1) * request.Size, take = request.Size });

            PageResultDto<MessageOutputDto> result = new PageResultDto<MessageOutputDto>
            {
                State = 1,
                Result = new ResultPage<MessageOutputDto>()
                {
                    TotalCount = (int)messages.TotalCount,
                    Data = messages.Data
                }
            };

            return result;
        }
    }

}
