using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Dapper.DapperWrapper
{
    /// <summary>
    /// IServiceCollection 扩展方法类
    /// </summary>
    public static class DapperDbContextServiceCollectionExtensions
    {
        /// <summary>
        /// IServiceCollection 扩展方法
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapperDbContext<TContext>(this IServiceCollection services, Action<DapperDbContextOptions> options)
            where TContext : DapperDbContext
        {
            services.AddOptions();
            services.Configure(options);
            services.AddScoped(typeof(TContext));
            services.AddScoped<DapperDbContext, TContext>();
            services.AddScoped<IUnitOfWork, DapperUnitOfWork<TContext>>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
