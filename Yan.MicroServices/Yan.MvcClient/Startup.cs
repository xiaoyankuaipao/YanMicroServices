using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yan.MvcClient.Clients;
using Yan.MvcClient.Message;

namespace Yan.MvcClient
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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); //cshtml 代码更改之后 不用重启服务 即可生效

            # region SignalR
            services.AddSignalR(options =>
            {
                //实时通信，需要发心跳包，就是双方都需要确定对方还在不在，若挂掉的话我好重连或者把你干掉啊，
                //所以就有了两个参数，一个是发心跳包的间隔时间，另一个就是等待对方心跳包的最长等待时间。
                //一般等待的时间设置成发心跳包的间隔时间的两倍即可
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });
            #endregion

            #region 跨域

            services.AddCors(options =>
            {
                options.AddPolicy("AnyCors",
                    policy =>
                    {
                        policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            #endregion

            #region 业务服务注入

            services.AddHttpClient();
            services.AddHttpClient<ArticleServiceClient>(client =>
            {
                //client.BaseAddress = new Uri("http://118.24.205.200:5000");
                client.BaseAddress = new Uri("http://localhost:5000");
            });
            services.AddHttpClient<BillClient>(client =>
            {
                //client.BaseAddress = new Uri("http://118.24.205.200:5000");
                client.BaseAddress = new Uri("http://localhost:5000");
            });

            #endregion

            #region 认证
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { })
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

                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:5100";
                    options.Audience = "article";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            if (!context.HttpContext.Request.Path.HasValue)
                            {
                                return Task.CompletedTask;
                            }

                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!(string.IsNullOrEmpty(accessToken)) && path.StartsWithSegments("/messagehub"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            #region 注入IHttpContextAccessor,方便获取HttpContext
            services.AddHttpContextAccessor();
            #endregion

            services.AddAuthorization();
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("AnyCors");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapWhen(x => x.Request.Path.StartsWithSegments("/api/articlemanage/src/Pictures"), builder =>
            {
                builder.Run( async context =>
                {
                    context.Response.Redirect("http://118.24.205.200:5000" + context.Request.Path);
                });
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();//添加WebSockets支持，SignalR优先使用WebSocket传输

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }
    }
}
