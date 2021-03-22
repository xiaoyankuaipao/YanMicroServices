// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using Yan.Consul;
using Yan.Idp.Data;
using Yan.Utility;

namespace Yan.Idp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                LocalInfo.ServerIp = Environment.GetEnvironmentVariable("HOST_IP");
                LocalInfo.SlbIp = Environment.GetEnvironmentVariable("SLB_IP");

                if (string.IsNullOrWhiteSpace(LocalInfo.ServerIp))
                {
                    LocalInfo.ServerIp = IPAddressHelper.GetLocalIP();
                }

                var host = CreateHostBuilder(args).Build();

                #region 初始化数据

                //Log.Information("Seeding database...");
                //var config = host.Services.GetRequiredService<IConfiguration>();
                //var connectionString = config.GetConnectionString("DefaultConnection");
                //SeedData.EnsureSeedData(connectionString);
                //Log.Information("Done seeding database.");

                #endregion

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5100");
                    webBuilder.UseStartup<Startup>();
                });
    }
}