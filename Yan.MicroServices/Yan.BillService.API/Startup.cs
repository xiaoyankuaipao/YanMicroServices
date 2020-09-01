using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yan.BillService.API.Application.Queries.Profiles;
using Yan.BillService.API.Extensions;
using Yan.Consul;
using Yan.Core.Filters;
using Yan.Core.Extensions;

namespace Yan.BillService.API
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
                    options.Audience = "bill";
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
                c.SwaggerEndpoint("/swagger/billmanage/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConsulHelper.RegisterService("http://127.0.0.1:8500", "dc1", "billmanage", "localhost", 6030).Wait();
        }
    }
}
