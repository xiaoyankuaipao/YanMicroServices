using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yan.Dapper;

namespace Yan.Idp
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mysqlConnection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string mysqlConnection)
        {
            services.AddTransient<DapperHelper>(proiver =>
            {
                var dapperHelper = new DapperHelper(mysqlConnection);
                return dapperHelper;
            });
            return services;
        }
    }
}
