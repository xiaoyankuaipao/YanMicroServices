using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yan.Core.Filters;
using Yan.Core.Extensions;
using AutoMapper;
using Yan.SystemService.API.Application.Queries.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yan.SystemService.API.Extensions;
using Yan.Consul;
using System.IdentityModel.Tokens.Jwt;

namespace Yan.SystemService.API
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
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
                //options.Filters.Add<ApiResultFilterAttribute>();
                options.Filters.Add<CustomExceptionAttribute>();
            });

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
                    options.Authority = "http://localhost:5100";
                    options.Audience = "system";
                    options.RequireHttpsMetadata = false;
                });

            services.AddMediatRServices();
            services.AddMySqlContext(Configuration["ConnectionStrings:MySqlConnection"]);
            services.AddRepositories();
            services.AddDapper(Configuration["ConnectionStrings:MySqlConnection"]);
            services.AddEventBus(Configuration);
            services.AddSwaggerDoc();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            ConsulHelper.RegisterService("http://127.0.0.1:8500", "dc1", "systemmanage", "localhost", 6020).Wait();
        }
    }
}
