// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using Yan.Consul;
using Yan.Idp.Data;
using Yan.Idp.Models;

namespace Yan.Idp
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IWebHostEnvironment Environment { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddHttpClient();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            });

            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryApiResources(Config.ApiResources);
            builder.AddInMemoryClients(Config.Clients);
            builder.AddAspNetIdentity<ApplicationUser>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;
            });
           
            builder.AddDeveloperSigningCredential();

            #region 腾讯云用
            //services.AddAuthentication()
            //       .AddGitHub(option =>
            //       {
            //           option.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //           option.ClientId = "f117b3563aad82f1ab2e";
            //           option.ClientSecret = "761f7a94755e5b15479a67c97609a56e18205103";
            //           option.Scope.Add("user:email");
            //       });
            #endregion

            //本次测试用
            services.AddAuthentication()
               .AddGitHub(option =>
               {
                   option.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                   option.ClientId = "5ecea3f522395ffe982d";
                   option.ClientSecret = "331b43cc58fa2b6e8765842f3aadaaab43e5b264";
                   option.Scope.Add("user:email");
               });

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("identityservice", new OpenApiInfo
                {
                    Title = "Yan.Idp",
                    Version = "v1"
                });
            });
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //options.SwaggerEndpoint($"/doc/identityservice/swagger.json", "identityservice");
                options.SwaggerEndpoint("/swagger/identityservice/swagger.json", "identityservice");
            });
            #endregion

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });


            //ConsulHelper.RegisterService("http://127.0.0.1:8500", "dc1", "identityservice", "localhost", 5100).Wait();
        }
    }
}