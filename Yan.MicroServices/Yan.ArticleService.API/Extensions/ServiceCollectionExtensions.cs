using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Yan.ArticleService.API.Application.IntegrationEvents;
using Yan.ArticleService.Domain.Aggregate.ArticleAggregate;
using Yan.ArticleService.Infrastructure;
using Yan.ArticleService.Infrastructure.Repositories;
using Yan.Dapper;

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

        #region Repository
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleTagRepository, ArticleTagRepository>();
            return services;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mysqlConnection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string mysqlConnection)
        {
            services.AddTransient<DapperHelper>(provider =>
            {
                var dapperHelper = new DapperHelper(mysqlConnection);
                return dapperHelper;
            });

            return services;
        }

        /// <summary>
        /// add Mediat service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ArticleContextTransactionBehavior<,>));//管道处理，如果有多个环节，要注意添加顺序，执行顺序与添加顺序相同
            return services.AddMediatR(typeof(Article).Assembly, typeof(Program).Assembly);
        }

        /// <summary>
        /// add DotNetCore.CAP service
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

        /// <summary>
        /// add Swagger doc service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("articlemanage", new OpenApiInfo { Title = "Article", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var openApiSecurity = new OpenApiSecurityScheme
                {
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",  //jwt 默认参数名称
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置（请求头）
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", openApiSecurity);
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

    }
}
