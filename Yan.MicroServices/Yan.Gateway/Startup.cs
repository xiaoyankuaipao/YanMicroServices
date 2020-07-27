using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Yan.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var config = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
            services.AddOcelot(config)
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                })
                .AddConsul()
                .AddPolly();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Yan.Gateway", new OpenApiInfo
                {
                    Title="Yan.Gatway",
                    Version="V1.0"
                });
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("kuayu", policy =>
            //    {
            //        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors("kuayu");

            #region Swagger
            var apiList = Configuration["Swagger:ServiceDocNames"].Split(',').ToList();
            app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                apiList.ForEach(apiItem =>
                {
                    options.SwaggerEndpoint($"/doc/{apiItem}/swagger.json", apiItem);
                });
            });
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
