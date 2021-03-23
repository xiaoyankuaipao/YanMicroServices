using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yan.Core.Filters;
using Yan.Core.Extensions;
using AutoMapper;
using Yan.SystemService.API.Application.Queries.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yan.SystemService.API.Extensions;
using Yan.Consul;
using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using Yan.SystemService.API.Modules;

namespace Yan.SystemService.API
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
        /// 
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

            //注入IHttpContextAccessor,方便获取HttpContext
            services.AddHttpContextAccessor();
            //制定控制器实例有容器来创建，方便属性注入，Controller本身默认是有MVC模块管理的
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://10.0.8.5:5100";
                    //options.Authority = "http://82.156.187.171:5100";
                    options.Audience = "system";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
                    IdentityModelEventSource.ShowPII = true;
                });


            services.AddMediatRServices();

            services.AddMySqlContext(Configuration["ConnectionStrings:MySqlConnection"]);
            services.AddRepositories();

            services.AddDapper(Configuration["ConnectionStrings:MySqlConnection"]);

            services.AddEventBus(Configuration);

            services.AddSwaggerDoc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //通过模块注册方式 注入依赖项
            builder.RegisterModule<WebModule>();
        }

        /// <summary>
        /// 
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
                c.SwaggerEndpoint("/swagger/systemmanage/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConsulHelper.RegisterService($"http://{LocalInfo.SlbIp}:8500", "dc1", "systemmanage", LocalInfo.ServerIp,
                6020).Wait();
        }
    }
}
