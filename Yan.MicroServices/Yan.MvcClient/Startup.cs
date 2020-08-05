using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yan.MvcClient.Clients;

namespace Yan.MvcClient
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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //cshtml 代码更改之后 不用重启服务 即可生效

            services.AddHttpClient();
            services.AddHttpClient<ArticleServiceClient>(client =>
            {
                client.BaseAddress = new Uri("http://118.24.205.200:5000");
            });


            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.Authority = "http://118.24.205.200:5100";
                options.Authority = "http://localhost:5100";
                options.RequireHttpsMetadata = false;
                options.ClientId = "Yan.MvcClient";
                options.ClientSecret = "Yan.MvcClient";
                options.SaveTokens = true;
                options.ResponseType = "code";
                options.Scope.Clear();
                options.Scope.Add("system");
                options.Scope.Add("article");
                options.Scope.Add(OidcConstants.StandardScopes.OpenId);
                options.Scope.Add(OidcConstants.StandardScopes.Profile);
                options.Scope.Add(OidcConstants.StandardScopes.Address);
                options.Scope.Add(OidcConstants.StandardScopes.Email);
                options.Scope.Add(OidcConstants.StandardScopes.Phone);
                // 与identity server的AllowOfflineAccess=true,对应。offline_access，指的是能否用refreshtoken重新申请令牌
                options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapWhen(x => x.Request.Path.StartsWithSegments("/api/articlemanage/src/Pictures"), builder =>
            {
                builder.Run(async context =>
                {
                     context.Response.Redirect("http://118.24.205.200:5000" + context.Request.Path);
                });
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
