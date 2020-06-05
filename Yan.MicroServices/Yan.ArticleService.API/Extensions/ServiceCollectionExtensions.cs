using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.ArticleService.API.Application.IntegrationEvents;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Infrastructure;
using Yan.ArticleService.Infrastructure.Repositories;

namespace Yan.ArticleService.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region DomainDbContext

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddDbContext<ArticleContext>(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySqlContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainDbContext(builder =>
            {
                builder.UseMySql(connectionString);
            });
        }

        #endregion

        #region 仓储
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            return services;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ArticleContextTransactionBehavior<,>));//管道处理，如果有多个环节，要注意添加顺序，执行顺序与添加顺序相同
            return services.AddMediatR(typeof(Article).Assembly, typeof(Program).Assembly);
        }

        /// <summary>
        /// DotNetCore.CAP
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            //添加继承事件订阅处理服务
            services.AddTransient<ISubscriberService, SubscriberService>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<ArticleContext>();

                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
            });
            return services;
        }

    }
}
