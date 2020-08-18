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
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Yan.ArticleService.API.Extensions;
using Yan.Core.Filters;
using Yan.Core.Extensions;
using Yan.Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using AutoMapper;
using Yan.ArticleService.API.Application.Queries.Profiles;
using Microsoft.Extensions.FileProviders;
using System.IdentityModel.Tokens.Jwt;

namespace Yan.ArticleService.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>(); 
                //options.Filters.Add<ApiResultFilterAttribute>();
                options.Filters.Add<CustomExceptionAttribute>();
            });

            //禁用默认行为,使用自定义验证过滤器
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;//禁用
            //});

            //推荐的模型验证方法
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var error = context.ModelState.GetValidationSummary();
                    return new ValidationFailedResult(error);
                };
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapProfiles>();
            });

            services.AddMediatRServices();

            services.AddMySqlContext(Configuration["ConnectionStrings:MySqlConnection"]);
            services.AddRepositories();

            services.AddDapper(Configuration["ConnectionStrings:MySqlConnection"]);

            services.AddEventBus(Configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:5100";
                    options.Audience = "article";
                    options.RequireHttpsMetadata = false;
                });

            services.AddSwaggerDoc();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/articlemanage/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/api/articlemanage/src")
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //ConsulHelper.RegisterService("http://127.0.0.1:8500", "dc1", "articlemanage", "localhost", 6010).Wait();
        }
    }
}
