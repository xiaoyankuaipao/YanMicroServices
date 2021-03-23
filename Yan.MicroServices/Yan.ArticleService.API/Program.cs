using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yan.Consul;

namespace Yan.ArticleService.API
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
                    webBuilder.UseUrls("http://*:6010");
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseKestrel(options =>
                    //{
                    //    options.ConfigureHttpsDefaults(i =>
                    //    {
                    //        i.ServerCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2("123.pfx", "123456");
                    //    });
                    //});
                });
    }
}
