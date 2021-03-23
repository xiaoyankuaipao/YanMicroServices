using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Yan.Consul;
using Yan.Utility;

namespace Yan.SystemService.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            LocalInfo.ServerIp = Environment.GetEnvironmentVariable("HOST_IP");
            LocalInfo.SlbIp = Environment.GetEnvironmentVariable("SLB_IP");


            if (string.IsNullOrWhiteSpace(LocalInfo.ServerIp))
            {
                LocalInfo.ServerIp = IPAddressHelper.GetLocalIP();
            }

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:6020");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
