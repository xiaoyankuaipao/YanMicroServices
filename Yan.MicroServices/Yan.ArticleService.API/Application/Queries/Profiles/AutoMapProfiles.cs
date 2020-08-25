using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.ArticleService.API.Models;
using Yan.ArticleService.Domain.Aggregate.ArticleCategoryAggregate;
using Yan.ArticleService.Domain.Aggregate.MessageAggregate;

namespace Yan.ArticleService.API.Application.Queries.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapProfiles: Profile
    {
        /// <summary>
        ///  
        /// </summary>
        public AutoMapProfiles()
        {
            CreateMap<ArticleCategory, ArticleCategoryDto>()
                .ForMember(m => m.Id, opts =>
                {
                    opts.MapFrom(c => c.Id);
                })
                .ForMember(m => m.Category, opts =>
                {
                    opts.MapFrom(c => c.Category);
                });


            // CreateMap<BaseOperateLogTemplate, BaseOperateLogOutput>()
            //.ForMember(m => m.LogTime, opts =>
            //{
            //    opts.MapFrom(c => c.LogTime.ToString("yyyy-MM-dd HH:mm:ss"));
            //});

            CreateMap<MessageAggregate, MessageOutputDto>()
                .ForMember(m => m.CreateTime, opts =>
                {
                    opts.MapFrom(c => c.CreateTime.ToString("yyyy-mm-dd HH:mm:ss"));
                });
        }
        
    }
}
